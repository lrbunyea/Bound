using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region Variables
    [SerializeField] GameObject minigameCanvas;
    [SerializeField] GameObject mainMenuCanvas;
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
    void Start()
    {
        //Initialize default minigame state
        HideMinigame();
        PopulateMinigameValues();

        //Register listeners for input events
        GameManager.Instance.MinigameStart.AddListener(ShowMinigame);
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
        GameManager.Instance.SetGameStateToPreMiniGame();
        AnalyticsManager.Instance.hasStarted = true;
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
