using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    public float radius;
    public bool firstTime;
    public string fullString;

    private int distanceBetween;
    private GameObject player;
    [SerializeField]
    private AILerp lerp;

    public Canvas myCanvas;
    public Text myText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstTime = true;
        lerp.canMove = false;
        myCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.pause)
        {
            lerp.canMove = false;
            return;
        }

        float distanceBetween = Vector2.Distance(transform.position, player.transform.position);

        if (distanceBetween < radius && firstTime == true)
        {
            lerp.canMove = true;
        }
        else if (distanceBetween > radius && firstTime == false)
        {
            firstTime = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myCanvas.gameObject.SetActive(true);
            GameManager.Instance.Pause(true);
            Debug.Log("We collided");
            firstTime = false;
            lerp.canMove = false;
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
