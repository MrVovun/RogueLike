using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;
using TMPro;

public class UIManager : MonoBehaviour {

    private PlayerController playerController;
    public Text moneyText;

    void OnEnable ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	void Update ()
    { 
        moneyText.text = "Money: " + playerController.money;
    }
}
