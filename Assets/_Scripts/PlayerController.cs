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

    void Start ()
    {
        playerCanMove = true;
        myCanvas.gameObject.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            myCanvas.gameObject.SetActive(true);
            StartCoroutine(ShowText());
        }
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
