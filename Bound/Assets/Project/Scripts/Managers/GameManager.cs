using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    public UnityEvent ConPenalty;
    public UnityEvent StartBlackScreen;
    public UnityEvent PrimingBlackScreen;
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
    private bool hasLogged;
    private bool firstPrompt;
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

        //Initialize all events - Must be in the awake function
        MinigameStart = new UnityEvent();
        CorrectWKeyPress = new UnityEvent();
        CorrectAKeyPress = new UnityEvent();
        CorrectSKeyPress = new UnityEvent();
        CorrectDKeyPress = new UnityEvent();
        ConPenalty = new UnityEvent();
        StartBlackScreen = new UnityEvent();
        PrimingBlackScreen = new UnityEvent();
        EndBlackScreen = new UnityEvent();
    }

    void Start()
    {
        ResetGameData();
        firstPrompt = true;

        //Register functions to events
        MinigameStart.AddListener(SetGameStateToMinigame);
        ConPenalty.AddListener(Fumble);
    }

    void Update () {
		if (currentState == GameState.Minigame)
        {
            //Check lose condition
            if (currentConsciousness <= 0 && !hasLogged)
            {
                EndBlackScreen.Invoke();
                AnalyticsManager.Instance.LogMinigameValues();
                AnalyticsManager.Instance.LogTimeSpentMinigame();
                AnalyticsManager.Instance.LogTotalTimeSpent();
                hasLogged = true;
            }
            DifficultyTimer();
            SpawnKey();
        }
	}
    #endregion

    #region Minigame Funtions
    /// <summary>
    /// Called to revoke player control from the player. Useful for minigame and menus.
    /// </summary>
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
        startingSpeed = 8f;
        currentSpeed = startingSpeed;
        currentSpeedMult = .5f;
        speedMultIncrement = 0.4f;
        startingSpawnTimer = 2f;
        currentSpawnTimer = startingSpawnTimer;
        spawnTimerDec = .2f;
        conLoss = 20;
        timeStageDuration = 8;
        currentTimeStage = 0;
    }

    /// <summary>
    /// Instantiates a random key from the allKeys array for the player to hit at the correct time.
    /// </summary>
    private void SpawnKey()
    {
        if (firstPrompt)
        {
            spawnTimeLeft = currentSpawnTimer;
            int item = Random.Range(0, allKeys.Length);
            Instantiate(allKeys[item], minigamePanel.transform);
            firstPrompt = false;
        } else
        {
            spawnTimeLeft -= Time.deltaTime;
            if (spawnTimeLeft < 0)
            {
                spawnTimeLeft = currentSpawnTimer;
                int item = Random.Range(0, allKeys.Length);
                Instantiate(allKeys[item], minigamePanel.transform);
            }
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
    /// Reloads the game when it ends. Called in the DialogueTextController script.
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Initialize proper values for the start of the game. Is also called when the game restarts.
    /// </summary>
    private void ResetGameData()
    {
        SetDefaultMinigameValues();
        //set current game state
        SetGameStateToMainMenu();
        currentConsciousness = 100;
        hasLogged = false;
        tsdTimeLeft = timeStageDuration;
        spawnTimeLeft = currentSpawnTimer;
    }

    /// <summary>
    /// Signals to the game manager that the player is in the main menu and to disable movement controls
    /// </summary>
    public void SetGameStateToMainMenu()
    {
        currentState = GameState.MainMenu;
        fpsCont.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
