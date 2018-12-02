using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static DialogueManager Instance;

    //Introduction narration
    private string[] introductionDialogue;  //Dialogue to be played during introductory black screen
    private string[] preMinigameDialogue;   //Dialogue to be played during preminigame sequence
    private string[] postMinigameDialogue;  //Dialogue to be played after players fail the minigame
    #endregion

    #region Unity API Functions
    void Awake () {
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

    private void Start()
    {
        introductionDialogue = new string[]
        {
            "Are you in any pain?",
            "No.",
            "Can you tell me what happened?",
            "I went on a run."
        };

        preMinigameDialogue = new string[] { "Every day after lunch, I used to fly down this sidewalk and up those stairs to the track.",
        "My friends were never able to catch up with me so they would just lean back on the chain link fence and watch. Flashing me a thumbs up when I blew by.",
        "I want that again.",
        "“Do not, under any circumstances, wear for more than eight hours a day.”",
        "“Do not wear during strenuous physical activity.”",
        "That’s what the package told me.",
        "Then, later, when I googled it, the internet told me the same thing.",
        "So, I said, “Fuck it” and now I’m here where I went to middle school like… eight years ago.",
        "Wearing this binder that I’m not supposed to and running shoes that I haven’t touched since I came out."};

        postMinigameDialogue = new string[]
        {
            "But I was wearing a binder and it was hard to breathe. So, I just… Passed out.",
            "Why didn’t you stop?",
            "I don’t know. I think I’m just tired of feeling helpless.",
            "Helpless? I’m afraid that I don’t understand."
        };
    }
    #endregion

    #region Accessible Functions
    /// <summary>
    /// Cycles through the intro dialogue, returning the next line in the sequence, then deleting it from the array.
    /// </summary>
    /// <returns>The next dialogue line for the player to read.</returns>
    public string GetNextIntroLine()
    {
        if (introductionDialogue.Length != 0)
        {
            string nextLine = introductionDialogue[0];
            string[] newDialogueQueue = introductionDialogue.Skip(1).ToArray();
            introductionDialogue = newDialogueQueue;
            return nextLine;
        }
        else
        {
            return "";
        }

    }

    /// <summary>
    /// Cycles through the preminigame dialogue, returning the next line in the sequence, then deleting it from the array.
    /// </summary>
    /// <returns>The next dialogue line for the player to read.</returns>
    public string GetNextPreminigameLine()
    {
        if (preMinigameDialogue.Length != 0)
        {
            string nextLine = preMinigameDialogue[0];
            string[] newDialogueQueue = preMinigameDialogue.Skip(1).ToArray();
            preMinigameDialogue = newDialogueQueue;
            return nextLine;
        } else
        {
            return "";
        }
        
    }

    /// <summary>
    /// Cycles through the post minigame dialogue, returning the next line in the sequence, then deleting it from the array.
    /// </summary>
    /// <returns>The next dialogue line for the player to read.</returns>
    public string GetNextPostLine()
    {
        if (postMinigameDialogue.Length != 0)
        {
            string nextLine = postMinigameDialogue[0];
            string[] newDialogueQueue = postMinigameDialogue.Skip(1).ToArray();
            postMinigameDialogue = newDialogueQueue;
            return nextLine;
        }
        else
        {
            return "";
        }

    }
    #endregion
}
