﻿using System;
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
        GameManager.Instance.StartCoroutine(ProcessBranch(dialogue, listOfSpeakers, "branch0"));   
	}

    private IEnumerator ProcessBranch(Dialogue dialogue, List<GameObject> listOfSpeakers, string branch) {
        DialogueMessage[] dialoguePiece;
        DialogueMessage dialogueLine;

        Branch branchObj;
        if (branch == null)
        {
            yield break;
        }
        dialogue.TryGetValue(branch, out branchObj);
        dialoguePiece = branchObj.dialogueMessages;

        foreach (GameObject speaker in listOfSpeakers)
        {
            if (speaker.name == branchObj.speaker)
            {
                dialogueLine = speaker.GetComponent<DialogueSpeaker>().PickLine(dialoguePiece);
                speaker.GetComponent<DialogueSpeaker>().Say(dialogueLine.dialogueMessage, delegate()
                {
                    GameManager.Instance.StartCoroutine(ProcessBranch(dialogue, listOfSpeakers, dialogueLine.branch));
                });           
            }
        }
    }
}
