using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {

    #region Variables
    [SerializeField] Text dialogueText;
    #endregion

    #region Unity API Functions
    private void OnTriggerEnter(Collider other)
    {
        //dialogueText.text = DialogueManager.Instance.GetNextLine();
        Destroy(this.gameObject);
    }
    #endregion
}
