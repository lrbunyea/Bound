using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnalyticsManager : MonoBehaviour {

    #region Variables
    //singleton pattern
    public static AnalyticsManager Instance;

    public bool hasStarted;

    private float timePlayed;
    private float timePreMinigame;
    private float timeMinigame;

    private string filePath;
    private StreamWriter writer;
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
        timePlayed = 0;
        hasStarted = false;
        //Open streamwriter to write analytics to a CSV file
        filePath = Application.dataPath + "PlayerData.csv";
        writer = new StreamWriter(filePath, true);

        //Register functions for events
        GameManager.Instance.MinigameStart.AddListener(LogTimeSpentPreMinigame);
	}
	
	void Update () {
        if (hasStarted)
        {
            timePlayed += Time.deltaTime;
        }
	}

    void OnDestroy()
    {
        //Close the stream writer
        writer.Flush();
        writer.Close();
    }
    #endregion

    #region Events To Track
    /// <summary>
    /// Called by the UI manager when the save button next the the player ID input field is pressed.
    /// </summary>
    /// <param name="id">The player's given ID</param>
    public void PlayerIDEntered(string id)
    {
        writer.WriteLine("PlayerID");
        writer.WriteLine(id);
        writer.WriteLine("");
    }

    /// <summary>
    /// Sends how many seconds the player has spent in the environment preceeding the track. Listens for the "MinigameStart" event.
    /// </summary>
    public void LogTimeSpentPreMinigame()
    {
        timePreMinigame = timePlayed;
        writer.WriteLine("TimeSpentPreMinigame");
        writer.WriteLine(timePreMinigame);
        writer.WriteLine("");
    }

    /// <summary>
    /// Sends how many seconds the player has spent playing the actual minigame. Is called by the GameManager when the minigame is lost.
    /// </summary>
    public void LogTimeSpentMinigame()
    {
        timeMinigame = timePlayed - timePreMinigame;
        writer.WriteLine("TimeSpentInMinigame");
        writer.WriteLine(timeMinigame);
        writer.WriteLine("");
    }

    /// <summary>
    /// Sends total amount of time the player has spent in the play session. Is called by the GameManager when the minigame is lost.
    /// </summary>
    public void LogTotalTimeSpent()
    {
        writer.WriteLine("TotalTimeSpent");
        writer.WriteLine(timePlayed);
        writer.WriteLine("");
        writer.WriteLine("");
    }

    /// <summary>
    /// Called by the Input manager when the correct breathe key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void CorrectBreatheKeyPressed(string key)
    {
        writer.WriteLine("Breathe,Correct,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the Input manager when the correct pace key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void CorrectPaceKeyPressed(string key)
    {
        writer.WriteLine("Pace,Correct,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the Input manager when an incorrect breathe key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void IncorrectBreatheKeyPressed(string key)
    {
        writer.WriteLine("Breathe,Incorrect,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Calced by the input manager when an incorrect pace key is pressed.
    /// </summary>
    /// <param name="key"></param>
    public void IncorrectPaceKeyPressed(string key)
    {
        writer.WriteLine("Pace,Incorrrect,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the GameManager right before the game ends. Records the minigame values that were set.
    /// </summary>
    public void LogMinigameValues()
    {
        writer.WriteLine("");
        writer.WriteLine("DifficultyMulitplierIncrement,TimeStageDuration,CorrectSpeed,CorrectOxygen,CorrectConsciousness,IncorrectSpeed,IncorrectOxygen,SpeedGain,OxygenLoss,ConsciousnessLoss");
        writer.WriteLine(GameManager.Instance.difficultyMultIncrement+","+GameManager.Instance.timeStageDuration+","+GameManager.Instance.correctSpeed+","+GameManager.Instance.correctOxygen+","+GameManager.Instance.correctCon+","+GameManager.Instance.incorrectSpeed+","+GameManager.Instance.incorrectOxygen+","+GameManager.Instance.speedGain+","+GameManager.Instance.oxygenLoss+","+GameManager.Instance.conLoss);
    }
    #endregion
}
