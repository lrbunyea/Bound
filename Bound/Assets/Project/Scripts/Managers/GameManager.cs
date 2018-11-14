using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        Minigame
    }

    //Potential player input keys
    private string[] allPaceKeys;
    private string[] allBreatheKeys;

    //Minigame events
    public UnityEvent CyclePaceKey;
    public UnityEvent CycleBreatheKey;
    public UnityEvent MinigameStart;

    //editable variables
    public float difficultyMultIncrement;
    public float timeStageDuration;
    public float correctSpeed;
    public float correctOxygen;
    public float correctCon;
    public float incorrectSpeed;
    public float incorrectOxygen;
    public float speedGain;
    public float oxygenLoss;
    public float conLoss;

    //public variables
    public float currentSpeed;
    public float currentOxygen;
    public float currentOxMod;
    public float currentConsciousness;
    public int currentConExp;
    public string currentPaceKey;
    public string currentBreatheKey;
    public float currentDifMult;
    public int currentTimeStage;
    public bool isBreatheCooldownActive;
    public GameState currentState;
    //private variables
    [SerializeField] float timeLeft;
    [SerializeField] GameObject fpsCont;
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

        allPaceKeys = new string[] { "W", "A", "S", "D" };
        allBreatheKeys = new string[] {"←", "→", "↑", "↓" };

        //Initialize minigame values
        SetDefaultMinigameValues();

        //Initialize all events - Must be in the awake function
        CyclePaceKey = new UnityEvent();
        CycleBreatheKey = new UnityEvent();
        MinigameStart = new UnityEvent();
    }

    void Start()
    {
        //set current game state
        SetGameStateToMainMenu();
        SetSlidersToDefault();
        SetRandomPaceKey();
        SetRandomBreatheKey();
        SetBreatheCooldownInactive();
        timeLeft = timeStageDuration;

        //Register functions to events
        Instance.MinigameStart.AddListener(SetGameStateToMinigame);
        InputManager.Instance.RunKeyPressed.AddListener(DecreaseSpeed);
        InputManager.Instance.IncorrectRunKeyPressed.AddListener(Fumble);
        InputManager.Instance.BreatheKeyPressed.AddListener(Breathe);
        InputManager.Instance.IncorrectBreatheKeyPressed.AddListener(Gasp);
    }

    void Update () {
		if (currentState == GameState.Minigame)
        {
            //Check to see if the current speed changes any oxygen modfiers
            if (50 < currentSpeed && currentSpeed <= 60)
            {
                currentOxMod = 1;
            }
            else if (60 < currentSpeed && currentSpeed <= 70)
            {
                currentOxMod = 2;
            }
            else if (70 < currentSpeed && currentSpeed <= 80)
            {
                currentOxMod = 3;
            }
            else if (80 < currentSpeed && currentSpeed <= 90)
            {
                currentOxMod = 4;
            }
            else if (90 < currentSpeed && currentSpeed <= 100)
            {
                currentOxMod = 5;
            }

            //Check to see if the current oxygen changes any consciousness modifiers
            if (90 < currentOxygen && currentOxygen <= 100)
            {
                currentConExp = 0;
            }
            else if (70 < currentOxygen && currentOxygen <= 90)
            {
                currentConExp = 1;
            }
            else if (50 < currentOxygen && currentOxygen <= 70)
            {
                currentConExp = 2;
            }
            else if (30 < currentOxygen && currentOxygen <= 50)
            {
                currentConExp = 3;
            }
            else if (0 < currentOxygen && currentOxygen <= 30)
            {
                currentConExp = 4;
            }

            //Finally, update meter values
            if (currentSpeed < 100)
            {
                currentSpeed += speedGain * currentDifMult * Time.deltaTime;
            }
            currentOxygen -= (oxygenLoss * currentOxMod) * currentDifMult * Time.deltaTime;


            //Check lose condition
            if (currentConsciousness > 0)
            {
                currentConsciousness -= Mathf.Pow(conLoss, currentConExp) * currentDifMult * Time.deltaTime;
            }
            else
            {
                AnalyticsManager.Instance.LogMinigameValues();
                AnalyticsManager.Instance.LogTimeSpentMinigame();
                AnalyticsManager.Instance.LogTotalTimeSpent();
                Application.Quit();
            }

            DifficultyTimer();
        }
	}
    #endregion

    #region Minigame Funtions
    /// <summary>
    /// Sets all minigame sliders to default value.
    /// </summary>
    public void SetSlidersToDefault()
    {
        currentSpeed = 50;
        currentOxygen = 100;
        currentOxMod = 1;
        currentConsciousness = 100;
        currentConExp = 0;
    }

    /// <summary>
    /// Sets default minigame multipliers so that the main menu can grab them for potential tweaking
    /// Will eventually pull from a save file
    /// </summary>
    private void SetDefaultMinigameValues()
    {
        currentDifMult = .5f;
        difficultyMultIncrement = 0.5f;
        timeStageDuration = 20;
        correctSpeed = 5;
        correctOxygen = 10;
        correctCon = 5;
        incorrectSpeed = 3;
        incorrectOxygen = 3;
        speedGain = 7;
        oxygenLoss = 2;
        conLoss = 2;
        currentTimeStage = 0;
    }

    /// <summary>
    /// Function called when the user presses the correct run button.
    /// </summary>
    private void DecreaseSpeed()
    {
        //Check to see if the value will go below the min
        if (currentSpeed <= 50)
        {
            return;
        }
        currentSpeed -= correctSpeed;
        SetRandomPaceKey();
        CyclePaceKey.Invoke();
    }

    /// <summary>
    /// Called when the user presses pace key that doesn't match the prompt.
    /// </summary>
    private void Fumble()
    {
        if (currentSpeed >= 100)
        {
            return;
        }
        currentSpeed += incorrectSpeed;
    }

    /// <summary>
    /// Function called when the user pauses to take a breath.
    /// </summary>
    private void Breathe()
    {
        //First check to make sure that user can input
        if (isBreatheCooldownActive)
        {
            return;
        } else
        {
            //Check to see if value will go beyond the max
            if (currentOxygen >= 100)
            {
                //do nothing
            }
            else
            {
                currentOxygen += correctOxygen;
            }
            if (currentConsciousness >= 100)
            {
                //do nothing
            }
            else
            {
                currentConsciousness += correctCon;
            }
            SetRandomBreatheKey();
            CycleBreatheKey.Invoke();
            StartBreatheCooldown();
        }
    }

    /// <summary>
    /// Called when the user presses breather key that doesn't match the prompt.
    /// </summary>
    private void Gasp()
    {
        if (currentOxygen <= 0)
        {
            return;
        } else
        {
            currentOxygen -= incorrectOxygen;
        }
    }

    /// <summary>
    /// Changes next pace key that player must input to random key from allPaceKeys array.
    /// </summary>
    private void SetRandomPaceKey()
    {
        int item = Random.Range(0, allPaceKeys.Length);
        currentPaceKey = allPaceKeys[item];
    }

    /// <summary>
    /// Changes next greath key that player must input to random key from allBreathKeys array.
    /// </summary>
    private void SetRandomBreatheKey()
    {
        int item = Random.Range(0, allBreatheKeys.Length);
        currentBreatheKey = allBreatheKeys[item];
    }

    /// <summary>
    /// Initiates breathe cooldown where the user cannot inpute any keys to breathe.
    /// </summary>
    private void StartBreatheCooldown()
    {
        isBreatheCooldownActive = true;
    }

    /// <summary>
    /// Keeps track what the minigame difficulty multiplier is.
    /// </summary>
    private void DifficultyTimer()
    {
        if (currentState == GameState.Minigame)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = timeStageDuration;
                currentDifMult += difficultyMultIncrement;
                currentTimeStage++;
            }
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
    /// Signals to the game manager, that the player has entered the minigame.
    /// </summary>
    public void SetGameStateToMinigame()
    {
        currentState = GameState.Minigame;
        fpsCont.GetComponent<FirstPersonController>().enabled = false;
        CyclePaceKey.Invoke();
        CycleBreatheKey.Invoke();

    }

    /// <summary>
    /// Sets the breathe cooldown flag to false.
    /// </summary>
    public void SetBreatheCooldownInactive()
    {
        isBreatheCooldownActive = false;
    }
    #endregion
}
