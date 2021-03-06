﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {

    public float radius;
    public bool firstTimeEntering = true;

    private int distanceBetween;
    private GameObject player;

    [SerializeField]
    private AILerp lerp;

    void Awake () {
        player = GameObject.FindGameObjectWithTag ("Player");
        lerp.canMove = false;
    }

    void Update () {
        if (GameManager.Instance.pause == true) {
            lerp.canMove = false;
        } else if (GameManager.Instance.pause == false) {
            float distanceBetween = Vector2.Distance (transform.position, player.transform.position);
            if (distanceBetween < radius && firstTimeEntering == true) {
                lerp.canMove = true;
            } else if (distanceBetween > radius && firstTimeEntering == false) {
                firstTimeEntering = true;
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            List<GameObject> newlist = new List<GameObject> { player, gameObject };
            Dialogue dialogue = DialogueLoader.Instance.DialogueSummon (gameObject.name);
            DialogueManager.Instance.StartDialogue (dialogue, newlist);
            firstTimeEntering = false;
            lerp.canMove = false;
        }
    }

}
