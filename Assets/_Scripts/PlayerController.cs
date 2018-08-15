using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int money;
    public bool playerCanMove;

    void Awake () {
        playerCanMove = true;
    }

    void Update () {
        if (GameManager.Instance.pause == true) {
            playerCanMove = false;
        } else if (GameManager.Instance.pause == false) {
            playerCanMove = true;
        }

        if (playerCanMove == true) {
            Movement ();
        }
    }

    private void Movement () {
        if (Input.GetKey (KeyCode.D)) {
            transform.Translate (Vector2.right * speed * Time.deltaTime);
        } else if (Input.GetKey (KeyCode.A)) {
            transform.Translate (Vector2.left * speed * Time.deltaTime);
        } else if (Input.GetKey (KeyCode.S)) {
            transform.Translate (Vector2.down * speed * Time.deltaTime);
        } else if (Input.GetKey (KeyCode.W)) {
            transform.Translate (Vector2.up * speed * Time.deltaTime);
        }
    }
}
