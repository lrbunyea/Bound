using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    #region Variables
    [SerializeField] GameObject minigameCanvas;
    [SerializeField] Slider speed;
    [SerializeField] Slider oxygen;
    [SerializeField] Slider consciousness;
    #endregion

    #region Unity API Functions
    void Start()
    {
        //Initialize default minigame state
        HideMinigame();

        //Register listeners for input events
        InputManager.Instance.MinigameStart.AddListener(ShowMinigame);
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
    #endregion
}
