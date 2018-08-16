using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpeaker : MonoBehaviour {

    public string fullString;
    public Canvas myCanvas;
    public TextMeshProUGUI myText;
    public float charTime = 0.1f;
    public GameObject answerButton;
    public int defaultWaitTime = 5;

    private List<GameObject> listOfAnswers = new List<GameObject> ();
    private int currentID;
    private bool playerTimeIsUp = false;
    private bool choosingMode = false;

    public event Action<DialogueMessage> OnAnswerConfirmed;

    public DialogueMessage selectedAnswer {
        get {
            return listOfAnswers.Find (l => l.GetComponent<CanBeSelected> ().isSelected).GetComponent<CanBeSelected> ().myDialogue;
        }
    }

    void Awake () {
        myCanvas.gameObject.SetActive (false);
        myText.gameObject.SetActive (false);
    }

    void changeSelected (int modifier) {
        int nextId = (currentID + modifier) % listOfAnswers.Count;
        if (nextId < 0) {
            nextId = listOfAnswers.Count - 1;
        }
        chooseButton (listOfAnswers[nextId]);
    }

    void Update () {
        if (gameObject.tag != "Player" || !choosingMode) return;

        bool Spressed = Input.GetKeyDown (KeyCode.S);
        bool Wpressed = Input.GetKeyDown (KeyCode.W);

        if (Spressed) {
            changeSelected (+1);
        } else if (Wpressed) {
            changeSelected (-1);
        }

        if (Input.GetKeyDown (KeyCode.Space)) {
            if (OnAnswerConfirmed != null) {
                OnAnswerConfirmed.Invoke (selectedAnswer);
                choosingMode = false;

                foreach (var item in listOfAnswers) {
                    Destroy (item);
                }
                listOfAnswers.Clear ();
            }
        }
    }

    IEnumerator WaitForAnswer (DialogueMessage[] arrayOfLines) {
        yield return new WaitForSeconds (defaultWaitTime);
        playerTimeIsUp = true;
        if (OnAnswerConfirmed != null) {
            OnAnswerConfirmed.Invoke (arrayOfLines[UnityEngine.Random.Range (0, arrayOfLines.Length)]);
        }
        Debug.Log ("Player's time is up!");
    }

    public void Say (string message, System.Action action) {
        fullString = message;
        myCanvas.gameObject.SetActive (true);
        StartCoroutine (ShowText (action));
    }

    public void StopSpeaking () {
        myCanvas.gameObject.SetActive (false);
        myText.gameObject.SetActive (false);
        myText.text = string.Empty;
    }

    IEnumerator ShowText (System.Action action) {
        myText.gameObject.SetActive (true);
        myText.text = string.Empty;
        foreach (char c in fullString) {
            myText.text += c;
            yield return new WaitForSeconds (charTime);
        }
        action.Invoke ();
    }

    public void PickLine (DialogueMessage[] arrayOfLines) {
        if (gameObject.tag == "Enemy") {
            OnAnswerConfirmed.Invoke (arrayOfLines[UnityEngine.Random.Range (0, arrayOfLines.Length)]);
        } else if (gameObject.tag == "Player") {
            StartCoroutine (WaitForAnswer (arrayOfLines));
            choosingMode = true;
            myCanvas.gameObject.SetActive (true);
            for (int i = 0; i < arrayOfLines.Length; i++) {
                GameObject chosenButton = Instantiate (answerButton, new Vector2 (0, 0), Quaternion.identity, myCanvas.transform);
                chosenButton.GetComponent<TextMeshProUGUI> ().text = arrayOfLines[i].dialogueMessage;
                if (i == 0) {
                    chooseButton (chosenButton);
                }
                chosenButton.GetComponent<CanBeSelected> ().myDialogue = arrayOfLines[i];
                listOfAnswers.Add (chosenButton);
            }
            StopCoroutine (WaitForAnswer (arrayOfLines));
        }
    }

    void chooseButton (GameObject button) {
        GameObject currentSelection = listOfAnswers.Find (l => l.GetComponent<CanBeSelected> ().isSelected);
        if (currentSelection != null) {
            currentSelection.GetComponent<CanBeSelected> ().isSelected = false;
        }

        button.GetComponent<CanBeSelected> ().isSelected = true;
        for (int i = 0; i < listOfAnswers.Count; i++) {
            if (listOfAnswers[i] == button) {
                currentID = i;
            }
        }
    }
}
