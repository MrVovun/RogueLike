using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float radius;    
    public bool firstTime;

    private int distanceBetween;
    private PlayerController playerController;
    private GameObject player;
    private GameManager gameManager;
    [SerializeField]
    private AILerp lerp;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        firstTime = true;
        lerp.canMove = false;
    }

    void Update()
    {
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
            Debug.Log("We collided");
            firstTime = false;
            lerp.canMove = false;
        }
    }
}
