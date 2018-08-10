using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class DialogueSpeaker : MonoBehaviour
{

    public string fullString;
    public Canvas myCanvas;
    public TextMeshProUGUI myText;
    public float charTime = 0.1f;
    public GameObject answerButton;

    private List<GameObject> listOfAnswers = new List<GameObject>();

    void Awake()
    {
        myCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < listOfAnswers.Count; i++)
        {
            listOfAnswers[0].GetComponent<CanBeSelected>().isSelected = true;
            if (listOfAnswers[i].GetComponent<CanBeSelected>().isSelected == true && Input.GetKey(KeyCode.S))
            {
                listOfAnswers[i].GetComponent<CanBeSelected>().isSelected = false;
                listOfAnswers[(i + 1) % listOfAnswers.Count].GetComponent<CanBeSelected>().isSelected = true;
            }
            else if (listOfAnswers[i].GetComponent<CanBeSelected>().isSelected == true && Input.GetKey(KeyCode.W))
            {
                listOfAnswers[i].GetComponent<CanBeSelected>().isSelected = false;
                int foo = (i - 1) % listOfAnswers.Count;
                if (i - 1 < 0)
                {
                    foo = listOfAnswers.Count - 1;
                }
                listOfAnswers[foo].GetComponent<CanBeSelected>().isSelected = true;
            }
        }
    }

    public void Say(string message, System.Action action)
    {
        fullString = message;
        myCanvas.gameObject.SetActive(true);
        StartCoroutine(ShowText(action));
    }

    IEnumerator ShowText(System.Action action)
    {
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
        if (gameObject.tag == "Enemy")
        {
            return arrayOfLines[Random.Range(0, arrayOfLines.Length)];
        }
        else if (gameObject.tag == "Player")
        {
            Vector3 buttonOffset = new Vector2 (0, -200);
            //fill arrayOfLines with answerButtons equal to arrayOfLines.Length
            //spawn with offset
            for (int i = 0; i < arrayOfLines.Length; i++)
            {

                answerButton.GetComponent<TextMeshProUGUI>().text = arrayOfLines[i].dialogueMessage;

                if (listOfAnswers[i].GetComponent<CanBeSelected>().isSelected == true && Input.GetKey(KeyCode.Space))
                {
                    //choose that answer
                    //return it below
                }
            }
            return arrayOfLines[someAnswerThatPlayerSelected];
        }
        else
        {
            return null;
        }

    }

}
