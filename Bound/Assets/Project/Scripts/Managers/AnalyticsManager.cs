using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AnalyticsManager : MonoBehaviour {

    #region Variables
    //singleton pattern
    public static AnalyticsManager Instance;

    public bool hasStarted;

    private float timePlayed;
    private float timePreMinigame;
    private float timeMinigame;
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
    }

    void Start () {
        GameAnalytics.Initialize();
        timePlayed = 0;
        hasStarted = false;

        //Register functions for events
        GameManager.Instance.MinigameStart.AddListener(LogTimeSpentPreMinigame);
	}
	
	void Update () {
        if (hasStarted)
        {
            timePlayed += Time.deltaTime;
        }
	}
    #endregion

    #region Events To Track
    /// <summary>
    /// Called by the UI manager when the save button next the the player ID input field is pressed.
    /// </summary>
    /// <param name="id">The player's given ID</param>
    public void PlayerIDEntered(string id)
    {
        GameAnalytics.NewDesignEvent("Player:" + id);
    }

    /// <summary>
    /// Sends how many seconds the player has spent in the environment preceeding the track. Listens for the "MinigameStart" event.
    /// </summary>
    public void LogTimeSpentPreMinigame()
    {
        timePreMinigame = timePlayed;
        GameAnalytics.NewDesignEvent("Time:PreMinigame:" + timePreMinigame);
    }

    /// <summary>
    /// Sends how many seconds the player has spent playing the actual minigame. Is called by the GameManager when the minigame is lost.
    /// </summary>
    public void LogTimeSpentMinigame()
    {
        timeMinigame = timePlayed - timePreMinigame;
        GameAnalytics.NewDesignEvent("Time:Minigame:" + timeMinigame);
    }

    /// <summary>
    /// Sends total amount of time the player has spent in the play session. Is called by the GameManager when the minigame is lost.
    /// </summary>
    public void LogTotalTimeSpent()
    {
        GameAnalytics.NewDesignEvent("Time:Total:" + timePlayed);
    }

    /// <summary>
    /// Called by the Input manager when the correct breathe key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void CorrectBreatheKeyPressed(string key)
    {
        GameAnalytics.NewDesignEvent("KeyPressed:Breathe:Correct:" + key, GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the Input manager when the correct pace key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void CorrectPaceKeyPressed(string key)
    {
        GameAnalytics.NewDesignEvent("KeyPressed:Pace:Correct:" + key, GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the Input manager when an incorrect breathe key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void IncorrectBreatheKeyPressed(string key)
    {
        GameAnalytics.NewDesignEvent("KeyPressed:Breathe:Incorrect:" + key, GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Calced by the input manager when an incorrect pace key is pressed.
    /// </summary>
    /// <param name="key"></param>
    public void IncorrectPaceKeyPressed(string key)
    {
        GameAnalytics.NewDesignEvent("KeyPressed:Pace:Incorrect:" + key, GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the GameManager right before the game ends. Records the minigame values that were set.
    /// </summary>
    public void LogMinigameValues()
    {
        GameAnalytics.NewDesignEvent("TestValues:Minigame:DifficultyMultiplierIncrement", GameManager.Instance.difficultyMultIncrement);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:TimeStageDuration", GameManager.Instance.timeStageDuration);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:CorrectSpeed", GameManager.Instance.correctSpeed);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:CorrectOxygen", GameManager.Instance.correctOxygen);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:CorrectConsciousness", GameManager.Instance.correctCon);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:IncorrectSpeed", GameManager.Instance.incorrectSpeed);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:IncorrectOxygen", GameManager.Instance.incorrectOxygen);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:SpeedGain", GameManager.Instance.speedGain);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:OxygenLoss", GameManager.Instance.oxygenLoss);
        GameAnalytics.NewDesignEvent("TestValues:Minigame:ConsciousnessLoss", GameManager.Instance.conLoss);
    }
    #endregion
}
