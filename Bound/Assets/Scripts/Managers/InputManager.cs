using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    #region InputEvents
    //Singleton pattern
    public static InputManager Instance;

    public UnityEvent MinigameStart;
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
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Event is invoked");
            MinigameStart.Invoke();
        }
	}
    #endregion
}
