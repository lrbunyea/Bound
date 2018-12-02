using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region Variables
    public static UIManager Instance;
    //UI object references
    [SerializeField] GameObject minigameCanvas;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject blackScreenPanel;
    //minigame values
    [SerializeField] InputField ssField;
    [SerializeField] InputField smiField;
    [SerializeField] InputField tsdField;
    [SerializeField] InputField sstField;
    [SerializeField] InputField stdField;
    [SerializeField] InputField clField;
    //player id
    [SerializeField] InputField playerID;
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

    void Start()
    {
        //Initialize default minigame state
        HideMinigame();
        PopulateMinigameValues();

        //Register listeners for input events
        GameManager.Instance.MinigameStart.AddListener(ShowMinigame);
        GameManager.Instance.EndBlackScreen.AddListener(FadeInPanel);
        GameManager.Instance.EndBlackScreen.AddListener(HideMinigame);
    }
    #endregion

    #region UI Manipulation Functions
    /// <summary>
    /// Enables the minigame UI to display on the screen.
    /// </summary>
    public void ShowMinigame()
    {
        minigameCanvas.SetActive(true);
    }

    /// <summary>
    /// Function that fades the black screen into gameplay. Called after intro dialogue and sounds are played.
    /// </summary>
    public void FadeOutPanel()
    {
        blackScreenPanel.GetComponent<Image>().CrossFadeColor(new Color(0, 0, 0, 0), 2.0f, false, true);
        GameManager.Instance.SetGameStateToPreMiniGame();
    }

    /// <summary>
    /// Function that fades into the black screen from gameplay. Called after the player fails the minigame.
    /// </summary>
    public void FadeInPanel()
    {
        blackScreenPanel.GetComponent<Image>().CrossFadeColor(new Color(0, 0, 0, 1), 3.0f, false, true);
    }

    /// <summary>
    /// Disables the minigame UI to display on the screen.
    /// </summary>
    public void HideMinigame()
    {
        minigameCanvas.SetActive(false);
    }

    /// <summary>
    /// Disables the main menu UI from showing on the screen
    /// </summary>
    public void HideMainMenu()
    {
        mainMenuCanvas.SetActive(false);
        AnalyticsManager.Instance.hasStarted = true;
        GameManager.Instance.StartBlackScreen.Invoke();
        GameManager.Instance.SetGameStateToBlackScreen();
    }

    /// <summary>
    /// Pulls what the current minigame values are from the game manager.
    /// </summary>
    private void PopulateMinigameValues()
    {
        ssField.text = GameManager.Instance.startingSpeed.ToString();
        smiField.text = GameManager.Instance.speedMultIncrement.ToString();
        tsdField.text = GameManager.Instance.timeStageDuration.ToString();
        sstField.text = GameManager.Instance.startingSpawnTimer.ToString();
        stdField.text = GameManager.Instance.spawnTimerDec.ToString();
        clField.text = GameManager.Instance.conLoss.ToString();
    }

    /// <summary>
    /// Informs the gamemanager which new values to use for the minigame
    /// </summary>
    public void SaveMinigameValues()
    {
        GameManager.Instance.speedMultIncrement = (float)Convert.ToDouble(ssField.text);
        GameManager.Instance.speedMultIncrement = (float)Convert.ToDouble(smiField.text);
        GameManager.Instance.timeStageDuration = (float)Convert.ToDouble(tsdField.text);
        GameManager.Instance.startingSpawnTimer = (float)Convert.ToDouble(sstField.text);
        GameManager.Instance.spawnTimerDec = (float)Convert.ToDouble(stdField.text);
        GameManager.Instance.conLoss = (float)Convert.ToDouble(clField.text);
    }

    /// <summary>
    /// Saves the player ID given to the player at the beginning of the session
    /// </summary>
    public void SavePlayerID()
    {
        AnalyticsManager.Instance.PlayerIDEntered(playerID.text);
    }
    #endregion
}
