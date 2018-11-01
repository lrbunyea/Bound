using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //public variables
    public float currentSpeed;
    public float currentOxygen;
    public float currentOxMod;
    public float currentConsciousness;
    public int currentConExp;
    public string currentPaceKey;
    public string currentBreatheKey;
    public float currentDifMult;
    public bool isBreatheCooldownActive;
    //private variables
    private GameState currentState;
    [SerializeField] float minigameDifficultyTimer;
    [SerializeField] float timeLeft;
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

        //Initialize all events - Must be in the awake function
        CyclePaceKey = new UnityEvent();
        CycleBreatheKey = new UnityEvent();
    }

    void Start()
    {
        //set current game state
        currentState = GameState.PreMinigame;
        SetSlidersToDefault();
        SetRandomPaceKey();
        SetRandomBreatheKey();
        SetBreatheCooldownInactive();
        timeLeft = minigameDifficultyTimer;
        currentDifMult = 1;

        //Register functions to events
        InputManager.Instance.MinigameStart.AddListener(SetGameStateToMinigame);
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
            if (currentSpeed < 93)
            {
                currentSpeed += 7 * currentDifMult * Time.deltaTime;
            }
            currentOxygen -= (2 * currentOxMod) * currentDifMult * Time.deltaTime;


            //Check lose condition
            if (currentConsciousness > 0)
            {
                currentConsciousness -= Mathf.Pow(2, currentConExp) * currentDifMult * Time.deltaTime;
            }
            else
            {
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
    /// Function called when the user presses the correct run button.
    /// </summary>
    private void DecreaseSpeed()
    {
        //Check to see if the value will go below the min
        if (currentSpeed <= 50)
        {
            return;
        }
        currentSpeed -= 5;
        SetRandomPaceKey();
        CyclePaceKey.Invoke();
    }

    /// <summary>
    /// Called when the user presses pace key that doesn't match the prompt.
    /// </summary>
    private void Fumble()
    {
        if (currentSpeed >= 97)
        {
            return;
        }
        currentSpeed += 3;
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
                currentOxygen += 10;
            }
            if (currentConsciousness >= 100)
            {
                //do nothing
            }
            else
            {
                currentConsciousness += 5;
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
        if (currentOxygen <= 3)
        {
            return;
        } else
        {
            currentOxygen -= 3;
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
                timeLeft = minigameDifficultyTimer;
                currentDifMult++;
            }
        }
    }
    #endregion

    #region Helper Functions
    /// <summary>
    /// Sets the game state to passed parameter.
    /// </summary>
    /// <param name="curState">New state to change the current game state to.</param>
    public void SetGameState(GameState curState)
    {
        currentState = curState;
    }

    /// <summary>
    /// Signals to the game manager, that the player has entered the minigame.
    /// </summary>
    public void SetGameStateToMinigame()
    {
        currentState = GameState.Minigame;
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
