using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    public float radius;
    public bool firstTimeEntering = true;
    public string fullString;

    private int distanceBetween;
    private GameObject player;

    public Canvas myCanvas;
    public TextMeshProUGUI myText;

    [SerializeField]
    private AILerp lerp;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (distanceBetween < radius && firstTimeEntering == true)
        {
            lerp.canMove = true;
        }
        else if (distanceBetween > radius && firstTimeEntering == false)
        {
            firstTimeEntering = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dialogue dialogue = DialogueLoader.Instance.DialogueSummon(gameObject.name);
            player.GetComponent<PlayerController>().Say(dialogue);
            fullString = dialogue.question[Random.Range (0, dialogue.question.Count)];
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
