using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextController : MonoBehaviour {

    #region Variables
    private Text dialogueText;
    //Change these to enum
    private bool isIntro;
    private bool isPreMinigame;
    private bool isPostMinigame;
    private bool waitingForCar; //True if the audio for xiting the car is currently playing
    private float timeLeft;     //The time left to display the current line on the screen
    private bool sameWord;      //True if we are still waiting for one line to be displayed for the proper amount of time
    string next;                //The line being displayed
    #endregion

    #region Unity API Functions
    void Start()
    {
        dialogueText = GetComponent<Text>();
        isIntro = false;
        isPreMinigame = false;
        isPostMinigame = false;
        sameWord = false;
        waitingForCar = false;
        //Register functions to events
        GameManager.Instance.StartBlackScreen.AddListener(PlayIntro);
        GameManager.Instance.EndBlackScreen.AddListener(PlayPostMinigame);
    }

    void Update () {

        if (waitingForCar)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                SoundManager.Instance.PlayMusic();
                UIManager.Instance.FadeOutPanel();
                waitingForCar = false;
            }
        } else
        {
            //Checking state
            if (isIntro)
            {
                //Check if we're waiting for the same word
                if (sameWord)
                {
                    WriteText(next);
                }
                else
                {
                    next = DialogueManager.Instance.GetNextIntroLine();
                    //Check to see if we are at the end of the section
                    if (next == "")
                    {
                        SoundManager.Instance.FadeOutHospital();
                        SoundManager.Instance.PlayCar();
                        waitingForCar = true;
                        timeLeft = SoundManager.Instance.carClipLength;
                        isIntro = false;
                        isPreMinigame = true;
                        dialogueText.text = "";
                    }
                    else
                    {
                        SoundManager.Instance.SwitchDialogueClip();
                        GetLineTimer();
                        WriteText(next);
                        sameWord = true;
                    }
                }
            }
            else if (isPreMinigame)
            {
                //Check if we're waiting for the same word
                if (sameWord)
                {
                    WriteText(next);
                }
                else
                {
                    next = DialogueManager.Instance.GetNextPreminigameLine();
                    //Check to see if we are at the end of the section
                    if (next == "")
                    {
                        isPreMinigame = false;
                        dialogueText.text = "";
                    }
                    else
                    {
                        SoundManager.Instance.SwitchDialogueClip();
                        GetLineTimer();
                        WriteText(next);
                        sameWord = true;
                    }
                }
            }
            else if (isPostMinigame)
            {
                //Check if we're waiting for the same word
                if (sameWord)
                {
                    WriteText(next);
                }
                else
                {
                    next = DialogueManager.Instance.GetNextPostLine();
                    //Check to see if we are at the end of the section
                    if (next == "")
                    {
                        //end the game
                        GameManager.Instance.ReloadScene();
                        
                        
                    }
                    else
                    {
                        SoundManager.Instance.SwitchDialogueClip();
                        GetLineTimer();
                        WriteText(next);
                        sameWord = true;
                    }
                }
            }
        }
	}
    #endregion

    #region Flag Functions
    /// <summary>
    /// Flags that the game is currently going through the intro sequence.
    /// Signals this chunk of dialogue should be played.
    /// </summary>
    private void PlayIntro()
    {
        isIntro = true;
    }

    /// <summary>
    /// Flags that the game is currently going through the preminigame sequence.
    /// Signals this chunk of dialogue should be played.
    /// </summary>
    private void PlayPreMinigame()
    {
        isPreMinigame = true;
    }

    /// <summary>
    /// Flags thay the game is currently going through the post minigame sequence.
    /// Signals this chunk of dialogue should be played.
    /// </summary>
    private void PlayPostMinigame()
    {
        isPostMinigame = true;
    }
    #endregion

    #region Text Writing Functions
    /// <summary>
    /// Writes the given text to the canvas for the appropriate amount of time.
    /// When the time has elapsed, sets the flag to false, indicating to move on to the next word in the section.
    /// </summary>
    /// <param name="newText">Text to be written to the canas.</param>
    private void WriteText(string newText)
    {
        //This is bad and being called every frame - need a better way to do this
        dialogueText.text = newText;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            sameWord = false;
        }
    }

    /// <summary>
    /// Calulates the proper amount of time to display a line of dialogue for based on character length.
    /// </summary>
    /// <param name="line">Line of dialogue to analyze.</param>
    private void GetLineTimer()
    {
        timeLeft = SoundManager.Instance.CurrentLineTime();
    }
    #endregion
}
