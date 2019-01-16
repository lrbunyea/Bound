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
        //Open streamwriter to write analytics to a CSV file
        filePath = Application.dataPath + "PlayerData.csv";
        writer = new StreamWriter(filePath, true);

        ResetAnalyticsData();

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
    /// <summary>
    /// Initialize proper values for the start of the game. Is also called when the game restarts.
    /// </summary>
    private void ResetAnalyticsData()
    {
        timePlayed = 0;
        hasStarted = false;
    }

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
    public void CorrectKeyPressed(string key)
    {
        writer.WriteLine("Correct,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the Input manager when an incorrect breathe key is pressed.
    /// </summary>
    /// <param name="key">Key that was pressed by the user</param>
    public void IncorrectKeyPressed(string key)
    {
        writer.WriteLine("Incorrect,"+key+","+GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the minigame killzone. It reports the prompt that the player failed to hit to the .CSV file.
    /// </summary>
    /// <param name="key">The prompt the player failed to hit.</param>
    public void KeyPassed(string key)
    {
        writer.WriteLine("Passed," + key + "," + GameManager.Instance.currentTimeStage);
    }

    /// <summary>
    /// Called by the GameManager right before the game ends. Records the minigame values that were set.
    /// </summary>
    public void LogMinigameValues()
    {
        writer.WriteLine("");
        writer.WriteLine("SpeedMulitplierIncrement,StartingSpeed,StartingSpawnTimer,SpawnTimerDecrement,TimeStageDuration,ConsciousnessLoss");
        writer.WriteLine(GameManager.Instance.speedMultIncrement+","+GameManager.Instance.startingSpeed+","+GameManager.Instance.startingSpawnTimer+","+GameManager.Instance.spawnTimerDec+","+GameManager.Instance.timeStageDuration+","+GameManager.Instance.conLoss);
        
    }
    #endregion
}
