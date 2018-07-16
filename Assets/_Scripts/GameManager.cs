using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool pause;

    private void Start()
    {
        pause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && pause == false)
        {
            PauseGame();
            pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && pause == true)
        {
            UnpauseGame();
            pause = false;
        }
    }


    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
