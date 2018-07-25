using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    public float radius;
    public bool firstTimeEntering = true;
    public string fullString;

    private int distanceBetween;
    private GameObject player;
    private DialogueLoader dialogueLoader;

    public Canvas myCanvas;
    public Text myText;

    [SerializeField]
    private AILerp lerp;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lerp.canMove = false;
        myCanvas.gameObject.SetActive(false);
        dialogueLoader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueLoader>();
    }

    void Update()
    {
        if (GameManager.Instance.pause)
        {
            lerp.canMove = false;
            return;
        }

        float distanceBetween = Vector2.Distance(transform.position, player.transform.position);
        if (distanceBetween < radius && firstTimeEntering == true)
        {
            lerp.canMove = true;
        }
        else if (distanceBetween > radius && firstTimeEntering == false)
        {
            firstTimeEntering = true;
        }
        if (dialogueLoader.dialogues.speaker == gameObject.name)
        {
            Debug.Log ("Ti pidor");
            fullString = dialogueLoader.dialogues.question[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myCanvas.gameObject.SetActive(true);
            Debug.Log(this.gameObject.name + "We collided");
            firstTimeEntering = false;
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
