using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN

    private static GameManager _instance;

    private static bool applicationIsQuitting = false;

    public static GameManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    public void OnDestroy()
    {
        Debug.Log("GameManager destroyed");
        applicationIsQuitting = true;
    }

    private void Awake()
    {
        applicationIsQuitting = false;
    }

    #endregion

    public bool pause = false;

    private void Start()
    {

    }

    void Update()
    {

    }

    public void Pause(bool needPause)
    {
        pause = needPause;
    }

}
