using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class DialogueLoader {

	#region SINGLETON PATTERN

    private static DialogueLoader _instance;

    private static bool applicationIsQuitting = false;

    public static DialogueLoader Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = new DialogueLoader();
            }

            return _instance;
        }
    }

    #endregion

	Dictionary<string, Dialogue> cache = new Dictionary<string, Dialogue>();

	public Dialogue DialogueSummon(string enemyName)
	{
		if(cache.ContainsKey(enemyName))
		{
			Dialogue dialogue;
			cache.TryGetValue(enemyName, out dialogue);
			return dialogue;
		}
		else
		{
			string loadedDialogue = JsonFileReader.JsonLoader("Dialogues/" + enemyName + ".json");
            Dialogue dialogue = JsonConvert.DeserializeObject<Dialogue>(loadedDialogue);
			cache.Add(enemyName, dialogue);
			return dialogue;
		}
	}
}
