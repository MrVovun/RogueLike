using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSpeaker : MonoBehaviour {

	public string fullString;
	public Canvas myCanvas;
    public TextMeshProUGUI myText;

	void Awake()
	{
		myCanvas.gameObject.SetActive(false);
	}

    public void Say(string message, System.Action action)
    {
        fullString = message;
        myCanvas.gameObject.SetActive(true);
        StartCoroutine(ShowText(action));
    }

    IEnumerator ShowText(System.Action action)
    {
        float charTime = 0.1f;
        myText.text = string.Empty;
        foreach (char c in fullString)
        {
            myText.text += c;
            yield return new WaitForSeconds(charTime);
		}
		action.Invoke();
    }

	public DialogueMessage PickLine(DialogueMessage[] arrayOfLines)
	{
		// if (gameObject.tag == "Enemy")
		// {
		 	DialogueMessage dialoguePhrase = arrayOfLines[Random.Range(0, arrayOfLines.Length)];
			return dialoguePhrase;
//		}
		// else if (gameObject.tag == "Player")
		// {
		// 	//let player choose an answer
			
		// }
	}

}
