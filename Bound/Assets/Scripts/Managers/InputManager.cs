using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region InputEvents
    //Singleton pattern
    public static InputManager Instance;

    //Input events
    public UnityEvent MinigameStart;
    public UnityEvent RunKeyPressed;
    public UnityEvent BreatheKeyPressed;
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
        MinigameStart = new UnityEvent();
        RunKeyPressed = new UnityEvent();
        BreatheKeyPressed = new UnityEvent();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MinigameStart.Invoke();
        }

        //Check conditions for steady pace
        if (Input.GetKeyDown(KeyCode.W) && GameManager.Instance.currentPaceKey == "W")
        {
            RunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentPaceKey == "E")
        {
            RunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R) && GameManager.Instance.currentPaceKey == "R")
        { 
            RunKeyPressed.Invoke();
        }

        //Check conditions for breathing
        if (Input.GetKeyDown(KeyCode.B) && GameManager.Instance.currentBreatheKey == "B")
        {
            BreatheKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.N) && GameManager.Instance.currentBreatheKey == "N")
        {
            BreatheKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.M) && GameManager.Instance.currentBreatheKey == "M")
        {
            BreatheKeyPressed.Invoke();
        }
    }
    #endregion
}
