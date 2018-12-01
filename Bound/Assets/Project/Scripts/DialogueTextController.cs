using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextController : MonoBehaviour {

    #region Variables
    private Text dialogueText;
    private bool isIntro;
    private bool isPreMinigame;
    private bool isPostMinigame;

    [SerializeField] float dialogueTimer;
    private float timeLeft;
    #endregion

    #region Unity API Functions
    void Start()
    {
        dialogueText = GetComponent<Text>();
        isIntro = false;
        isPreMinigame = false;
        isPostMinigame = false;

        //Register functions to events
        GameManager.Instance.StartBlackScreen.AddListener(PlayIntro);
        GameManager.Instance.EndBlackScreen.AddListener(PlayPostMinigame);
    }

    void Update () {
        if (isIntro)
        {
            string next = DialogueManager.Instance.GetNextIntroLine();
            if (next != "")
            {
                WriteText(next);
            } else
            {
                isIntro = false;
                UIManager.Instance.FadeOutPanel();
                isPreMinigame = true;
            }
        } else if (isPreMinigame)
        {

        } else if (isPostMinigame)
        {

        }
	}

    private void PlayIntro()
    {
        isIntro = true;
    }

    private void WriteText(string newText)
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = dialogueTimer;
            dialogueText.text = newText;
        }
    }

    private void PlayPreMinigame()
    {
        isPreMinigame = true;
    }

    private void PlayPostMinigame()
    {
        isPostMinigame = true;
    }
    #endregion
}
