using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private PlayerController player;
    public Text moneyText;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	void Update ()
    { 
        moneyText.text = "Money: " + player.money;
    }
}
