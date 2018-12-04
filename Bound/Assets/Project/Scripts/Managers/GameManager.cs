using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static GameManager Instance;

    //Game state enum
    public enum GameState
    {
        MainMenu,
        PauseMenu,
        PreMinigame,
        Minigame,
        Blackscreen
    }

    //Potential player input keys
    [SerializeField] GameObject[] allKeys;

    //Minigame events
    public UnityEvent MinigameStart;
    public UnityEvent CorrectKeyPress;
    public UnityEvent CorrectWKeyPress;
    public UnityEvent CorrectAKeyPress;
    public UnityEvent CorrectSKeyPress;
    public UnityEvent CorrectDKeyPress;
    public UnityEvent IncorrectKeyPress;
    public UnityEvent StartBlackScreen;
    public UnityEvent EndBlackScreen;

    //editable variables
    public float startingSpeed;
    public float speedMultIncrement;
    public float timeStageDuration;
    public float startingSpawnTimer;
    public float spawnTimerDec;
    public float conLoss;

    //public variables
    public float currentConsciousness;
    public float currentSpeedMult;
    public float currentSpeed;
    public float currentSpawnTimer;
    public int currentTimeStage;
    public GameState currentState;
    //private variables
    [SerializeField] float tsdTimeLeft;
    [SerializeField] float spawnTimeLeft;
    [SerializeField] GameObject fpsCont;
    [SerializeField] GameObject minigamePanel;
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

        //Initialize minigame values
        SetDefaultMinigameValues();

        //Initialize all events - Must be in the awake function
        MinigameStart = new UnityEvent();
        CorrectWKeyPress = new UnityEvent();
        CorrectAKeyPress = new UnityEvent();
        CorrectSKeyPress = new UnityEvent();
        CorrectDKeyPress = new UnityEvent();
        IncorrectKeyPress = new UnityEvent();
        StartBlackScreen = new UnityEvent();
        EndBlackScreen = new UnityEvent();
    }

    void Start()
    {
        //set current game state
        SetGameStateToMainMenu();
        currentConsciousness = 100;
        tsdTimeLeft = timeStageDuration;
        spawnTimeLeft = currentSpawnTimer;

        //Register functions to events
        MinigameStart.AddListener(SetGameStateToMinigame);
        IncorrectKeyPress.AddListener(Fumble);
    }

    void Update () {
		if (currentState == GameState.Minigame)
        {
            //Check lose condition
            if (currentConsciousness <= 0)
            {
                EndBlackScreen.Invoke();
                AnalyticsManager.Instance.LogMinigameValues();
                AnalyticsManager.Instance.LogTimeSpentMinigame();
                AnalyticsManager.Instance.LogTotalTimeSpent();
            }
            DifficultyTimer();
            SpawnKey();
        }
	}
    #endregion

    #region Minigame Funtions
    public void DisablePlayerMovement()
    {
        fpsCont.GetComponent<FirstPersonController>().enabled = false;
    }

    /// <summary>
    /// Sets default minigame multipliers so that the main menu can grab them for potential tweaking
    /// Will eventually pull from a save file (Maybe)
    /// </summary>
    private void SetDefaultMinigameValues()
    {
        startingSpeed = 2;
        currentSpeed = startingSpeed;
        currentSpeedMult = .5f;
        speedMultIncrement = 0.3f;
        startingSpawnTimer = 1f;
        currentSpawnTimer = startingSpawnTimer;
        spawnTimerDec = .1f;
        conLoss = 20;
        timeStageDuration = 20;
        currentTimeStage = 0;
    }

    /// <summary>
    /// Instantiates a random key from the allKeys array for the player to hit at the correct time.
    /// </summary>
    private void SpawnKey()
    {
        spawnTimeLeft -= Time.deltaTime;
        if (spawnTimeLeft < 0)
        {
            spawnTimeLeft = currentSpawnTimer;
            int item = Random.Range(0, allKeys.Length);
            Instantiate(allKeys[item], minigamePanel.transform);
        }
    }

    /// <summary>
    /// A function that is invoked when the player presses an incorrect key.
    /// </summary>
    private void Fumble()
    {
        currentConsciousness -= conLoss;
    }

    /// <summary>
    /// Keeps track what the minigame difficulty multiplier is.
    /// </summary>
    private void DifficultyTimer()
    {
        tsdTimeLeft -= Time.deltaTime;
        if (tsdTimeLeft < 0)
        {
            tsdTimeLeft = timeStageDuration;
            currentSpeedMult += speedMultIncrement;
            currentSpawnTimer -= spawnTimerDec;
            currentTimeStage++;
        }
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Signals to the game manager that the player is in the main menu and to disable movement controls
    /// </summary>
    public void SetGameStateToMainMenu()
    {
        currentState = GameState.MainMenu;
        fpsCont.GetComponent<FirstPersonController>().enabled = false;
    }


    /// <summary>
    /// Signals to the game manager to enable the player to walk around the space.
    /// </summary>
    public void SetGameStateToPreMiniGame()
    {
        currentState = GameState.PreMinigame;
        fpsCont.GetComponent<FirstPersonController>().enabled = true;
    }

    /// <summary>
    /// Signals to the game manager that the player has entered the minigame.
    /// </summary>
    public void SetGameStateToMinigame()
    {
        currentState = GameState.Minigame;
        fpsCont.GetComponent<FirstPersonController>().enabled = false;

    }

    /// <summary>
    /// Signals to the game manager that a black screen is currently being displayed.
    /// </summary>
    public void SetGameStateToBlackScreen()
    {
        currentState = GameState.Blackscreen;
        fpsCont.GetComponent<FirstPersonController>().enabled = false;
    }
    #endregion
}
