using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region InputEvents
    //Singleton pattern
    public static InputManager Instance;

    //Input events
    
    public UnityEvent RunKeyPressed;
    public UnityEvent IncorrectRunKeyPressed;
    public UnityEvent BreatheKeyPressed;
    public UnityEvent IncorrectBreatheKeyPressed;
    #endregion

    #region Unity API Functions
    void Awake()
    {
        //Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //Initialize all events - Must be in the awake function
        RunKeyPressed = new UnityEvent();
        IncorrectRunKeyPressed = new UnityEvent();
        BreatheKeyPressed = new UnityEvent();
    }

    void Update () {

        if (GameManager.Instance.currentState == GameManager.GameState.Minigame)
        {
            //Check conditions for steady pace
            if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.currentPaceKey == "W")
            {
                AnalyticsManager.Instance.CorrectPaceKeyPressed("W");
                RunKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.currentPaceKey != "W")
            {
                AnalyticsManager.Instance.IncorrectPaceKeyPressed("W");
                IncorrectRunKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.currentPaceKey == "A")
            {
                AnalyticsManager.Instance.CorrectPaceKeyPressed("A");
                RunKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.currentPaceKey != "A")
            {
                AnalyticsManager.Instance.IncorrectPaceKeyPressed("A");
                IncorrectRunKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.currentPaceKey == "S")
            {
                AnalyticsManager.Instance.CorrectPaceKeyPressed("S");
                RunKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.currentPaceKey != "S")
            {
                AnalyticsManager.Instance.IncorrectPaceKeyPressed("S");
                IncorrectRunKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.currentPaceKey == "D")
            {
                AnalyticsManager.Instance.CorrectPaceKeyPressed("D");
                RunKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.currentPaceKey != "D")
            {
                AnalyticsManager.Instance.IncorrectPaceKeyPressed("D");
                IncorrectRunKeyPressed.Invoke();
            }

            //Check conditions for breathing
            if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.currentBreatheKey == "↑")
            {
                AnalyticsManager.Instance.CorrectBreatheKeyPressed("UpArrow");
                BreatheKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.currentBreatheKey != "↑")
            {
                AnalyticsManager.Instance.IncorrectBreatheKeyPressed("UpArrow");
                IncorrectBreatheKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.currentBreatheKey == "↓")
            {
                AnalyticsManager.Instance.CorrectBreatheKeyPressed("DownArrow");
                BreatheKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.currentBreatheKey != "↓")
            {
                AnalyticsManager.Instance.IncorrectBreatheKeyPressed("DownArrow");
                IncorrectBreatheKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.currentBreatheKey == "←")
            {
                AnalyticsManager.Instance.CorrectBreatheKeyPressed("LeftArrow");
                BreatheKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.currentBreatheKey != "←")
            {
                AnalyticsManager.Instance.IncorrectBreatheKeyPressed("LeftArrow");
                IncorrectBreatheKeyPressed.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.currentBreatheKey == "→")
            {
                AnalyticsManager.Instance.CorrectBreatheKeyPressed("RightArrow");
                BreatheKeyPressed.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.currentBreatheKey != "→")
            {
                AnalyticsManager.Instance.IncorrectBreatheKeyPressed("RightArrow");
                IncorrectBreatheKeyPressed.Invoke();
            }
        }
    }
    #endregion
}
