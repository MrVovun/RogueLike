using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Movement();
	}

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float veticalInput = Input.GetAxis("Vertical");
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * speed * veticalInput);
        }
    }

    }
