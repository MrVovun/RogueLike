using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private GameObject player;

    public float radius;
    public int price;
    public int speed;
    private bool firstTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstTime = true;
    }

    void Update()
    {
        float distanceBetween = Vector2.Distance(this.transform.position, player.transform.position);
        if (distanceBetween < radius && firstTime == true)
        {
            MoveTo();
        }
        else if (distanceBetween > radius && firstTime == false)
        {
            firstTime = true;
        }
    }

    private void MoveTo()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("We collided");
            firstTime = false;
        }
    }
}
