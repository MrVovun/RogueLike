using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSpeaker : MonoBehaviour
{

    public string fullString;
    public Canvas myCanvas;
    public TextMeshProUGUI myText;
    public float charTime = 0.1f;
    public TextMeshProUGUI answerButton;

    private List<GameObject> listOfAnswers = new List<GameObject>();

    void Awake()
    {
        myCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < listOfAnswers.Count; i++)
        {
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
            for (int i = 0; i < arrayOfLines.Length; i++)
            {
                TextMeshProUGUI answer;
                Vector3 buttonOffset = new Vector2 (0, -200);

                answer = Instantiate(answerButton, transform.position + buttonOffset, transform.rotation) as TextMeshProUGUI;
                answer.text = arrayOfLines[i].dialogueMessage;
                //get array from update
            }
        }
        else
        {
            return null;
        }

    }

}
