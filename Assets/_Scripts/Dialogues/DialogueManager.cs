using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager {
	#region SINGLETON PATTERN

    private static DialogueManager _instance;

    private static bool applicationIsQuitting = false;

    public static DialogueManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = new DialogueManager();
            }

            return _instance;
        }
    }

    #endregion

	public void StartDialogue(Dialogue dialogue, List<GameObject> listOfSpeakers)
	{
        string branch = "branch0";
        DialogueMessage[] dialoguePiece;
        DialogueMessage dialogueLine;
        
        while(true)
        {
            Branch branchObj;
            dialogue.TryGetValue(branch, out branchObj);
            dialoguePiece = branchObj.dialogueMessages;

            foreach (GameObject speaker in listOfSpeakers)
            {
                if (speaker.name == branchObj.speaker)
                {
                    dialogueLine = speaker.GetComponent<DialogueSpeaker>().PickLine(dialoguePiece);
                    branch = dialogueLine.branch;
                    speaker.GetComponent<DialogueSpeaker>().Say(dialogueLine.dialogueMessage);
                    if (branch == null)
                    {
                        return;
                    }
                }
            }
        }
	}
}
