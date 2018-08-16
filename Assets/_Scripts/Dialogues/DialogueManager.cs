using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager {
    #region SINGLETON PATTERN

    private static DialogueManager _instance;

    private static bool applicationIsQuitting = false;

    public static DialogueManager Instance {
        get {
            if (applicationIsQuitting) {
                return null;
            }

            if (_instance == null) {
                _instance = new DialogueManager ();
            }

            return _instance;
        }
    }

    #endregion

    public void StartDialogue (Dialogue dialogue, List<GameObject> listOfSpeakers) {
        GameManager.Instance.StartCoroutine (ProcessBranch (dialogue, listOfSpeakers, "branch0"));
        GameManager.Instance.Pause (true);
    }

    public void EndDialogue (List<GameObject> listOfSpeakers) {
        foreach (GameObject speaker in listOfSpeakers) {
            speaker.GetComponent<DialogueSpeaker> ().StopSpeaking ();
        }
        GameManager.Instance.Pause (false);
    }

    private IEnumerator ProcessBranch (Dialogue dialogue, List<GameObject> listOfSpeakers, string branch) {
        Branch branchObj;
        if (branch == null) {
            yield break;
        }
        dialogue.TryGetValue (branch, out branchObj);
        DialogueMessage[] dialoguePiece = branchObj.dialogueMessages;

        foreach (GameObject speaker in listOfSpeakers) {
            if (speaker.name == branchObj.speaker) {
                Action<DialogueMessage> onAnswerPicked = null;

                onAnswerPicked = (DialogueMessage message) => {
                    speaker.GetComponent<DialogueSpeaker> ().OnAnswerConfirmed -= onAnswerPicked;
                    speaker.GetComponent<DialogueSpeaker> ().Say (message.dialogueMessage, () => {
                        GameManager.Instance.StartCoroutine (ProcessBranch (dialogue, listOfSpeakers, message.branch));
                        if (message.branch == null) {
                            EndDialogue (listOfSpeakers);
                        }
                    });
                };

                speaker.GetComponent<DialogueSpeaker> ().OnAnswerConfirmed += onAnswerPicked;
                speaker.GetComponent<DialogueSpeaker> ().PickLine (dialoguePiece);
            }
        }
    }
}
