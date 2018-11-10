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
    [SerializeField] InputField dmiField;
    [SerializeField] InputField tsdField;
    [SerializeField] InputField csField;
    [SerializeField] InputField coField;
    [SerializeField] InputField ccField;
    [SerializeField] InputField icsField;
    [SerializeField] InputField icoField;
    [SerializeField] InputField sgField;
    [SerializeField] InputField olField;
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
    }

    /// <summary>
    /// Pulls what the current minigame values are from the game manager.
    /// </summary>
    private void PopulateMinigameValues()
    {
        dmiField.text = GameManager.Instance.difficultyMultIncrement.ToString();
        tsdField.text = GameManager.Instance.timeStageDuration.ToString();
        csField.text = GameManager.Instance.correctSpeed.ToString();
        coField.text = GameManager.Instance.correctOxygen.ToString();
        ccField.text = GameManager.Instance.correctCon.ToString();
        icsField.text = GameManager.Instance.incorrectSpeed.ToString();
        icoField.text = GameManager.Instance.incorrectOxygen.ToString();
        sgField.text = GameManager.Instance.speedGain.ToString();
        olField.text = GameManager.Instance.oxygenLoss.ToString();
    }

    /// <summary>
    /// Informs the gamemanager which new values to use for the minigame
    /// </summary>
    public void SaveMinigameValues()
    {
        GameManager.Instance.difficultyMultIncrement = (float)Convert.ToDouble(dmiField.text);
        GameManager.Instance.timeStageDuration = (float)Convert.ToDouble(tsdField.text);
        GameManager.Instance.correctSpeed = (float)Convert.ToDouble(csField.text);
        GameManager.Instance.correctOxygen = (float)Convert.ToDouble(coField.text);
        GameManager.Instance.correctCon = (float)Convert.ToDouble(ccField.text);
        GameManager.Instance.incorrectSpeed = (float)Convert.ToDouble(icsField.text);
        GameManager.Instance.incorrectOxygen = (float)Convert.ToDouble(icoField.text);
        GameManager.Instance.speedGain = (float)Convert.ToDouble(sgField.text);
        GameManager.Instance.oxygenLoss = (float)Convert.ToDouble(olField.text);
    }

    /// <summary>
    /// Saves the player ID given to the player at the beginning of the session
    /// </summary>
    public void SavePlayerID()
    {
        //write to file
    }
    #endregion
}
