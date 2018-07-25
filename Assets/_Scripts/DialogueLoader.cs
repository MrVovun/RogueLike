using System.Collections;
using UnityEngine;

public class DialogueLoader : MonoBehaviour {

	public Dialogue dialogues;

	void Awake()
	{
		string loadedDialogue = JsonFileReader.JsonLoader("Dialogues/test.json");
		dialogues = JsonUtility.FromJson<Dialogue>(loadedDialogue);
	}
}
