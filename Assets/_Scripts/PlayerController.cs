using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int money;
    public bool playerCanMove;
    public string fullString;

    public Canvas myCanvas;
    public Text myText;

    private DialogueLoader dialogueLoader;

    void Awake ()
    {
        myCanvas.gameObject.SetActive(false);
        playerCanMove = true;
    }
	
	void Update ()
    {
        if (GameManager.Instance.pause)
        {
            playerCanMove = false;
            return;
        }
        if (playerCanMove == true)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    public void Say(Dialogue dialogue)
    {
            fullString = dialogue.answer[Random.Range(0, dialogue.answer.Count)];
            myCanvas.gameObject.SetActive(true);
            StartCoroutine(ShowText());
            GameManager.Instance.Pause(true);
    }

    IEnumerator ShowText()
    {
        float charTime = 2.0f / fullString.Length;
        myText.text = string.Empty;
        foreach (char c in fullString)
        {
            myText.text += c;
            yield return new WaitForSeconds(charTime);
        }
    }
}
