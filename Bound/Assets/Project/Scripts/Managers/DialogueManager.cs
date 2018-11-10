using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    #region Variables
    //Singleton pattern
    public static DialogueManager Instance;

    //Introduction narration
    private string[] introductionDialogue;
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
        introductionDialogue = new string[] { "Every day after lunch, I used to fly down this sidewalk and up those stairs to the track.",
        "My friends were never able to catch up with me so they would just lean back on the chain link fence and watch. Flashing me a thumbs up when I blew by.",
        "I want that again.",
        "“Do not, under any circumstances, wear for more than eight hours a day.”",
        "“Do not wear during strenuous physical activity.”",
        "That’s what the package told me.",
        "Then, later, when I googled it, the internet told me the same thing.",
        "So, I said, “Fuck it” and now I’m here where I went to middle school like… eight years ago.",
        "Wearing this binder that I’m not supposed to and running shoes that I haven’t touched since I came out."};
    }
    #endregion

    #region Accessible Functions
    /// <summary>
    /// Function to be called by the dialuge triggers to populate UI text.
    /// </summary>
    /// <returns>The next dialogue line for the player to read.</returns>
    public string GetNextLine()
    {
        if (introductionDialogue.Length != 0)
        {
            string nextLine = introductionDialogue[0];
            string[] newDialogueQueue = introductionDialogue.Skip(1).ToArray();
            introductionDialogue = newDialogueQueue;
            return nextLine;
        } else
        {
            return "";
        }
        
    }
    #endregion
}
