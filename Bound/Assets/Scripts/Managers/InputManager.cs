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
        if (Input.GetKeyDown(KeyCode.W))
        {
            RunKeyPressed.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreatheKeyPressed.Invoke();
        }
	}
    #endregion
}
