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

        //Check conditions for steady pace
        if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.currentPaceKey == "W")
        {
            RunKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.currentPaceKey != "W")
        {
            IncorrectRunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.currentPaceKey == "A")
        {
            RunKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.A) && GameManager.Instance.currentPaceKey != "A")
        {
            IncorrectRunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.currentPaceKey == "S")
        { 
            RunKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.S) && GameManager.Instance.currentPaceKey != "S")
        {
            IncorrectRunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.currentPaceKey == "D")
        {
            RunKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.D) && GameManager.Instance.currentPaceKey != "D")
        {
            IncorrectRunKeyPressed.Invoke();
        }

        //Check conditions for breathing
        if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.currentBreatheKey == "↑")
        {
            BreatheKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.currentBreatheKey != "↑")
        {
            IncorrectBreatheKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.currentBreatheKey == "↓")
        {
            BreatheKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && GameManager.Instance.currentBreatheKey != "↓")
        {
            IncorrectBreatheKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.currentBreatheKey == "←")
        {
            BreatheKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && GameManager.Instance.currentBreatheKey != "←")
        {
            IncorrectBreatheKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.currentBreatheKey == "→")
        {
            BreatheKeyPressed.Invoke();
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && GameManager.Instance.currentBreatheKey != "→")
        {
            IncorrectBreatheKeyPressed.Invoke();
        }
    }
    #endregion
}
