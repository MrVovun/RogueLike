using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private PlayerController playerController;
    private GameObject player;

    public float radius;
    public int price;
    public int speed;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distanceBetween = Vector2.Distance (this.transform.position, player.transform.position);
        if (distanceBetween < radius)
        {
            MoveTo();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerController.money = playerController.money - price;
            //wait for some time
            //look for player again
        }
    }

    private void MoveTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
    }
}
