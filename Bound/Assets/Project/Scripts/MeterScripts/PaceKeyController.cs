using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaceKeyController : MonoBehaviour {

    #region Variables
    Text paceKeyText;
    #endregion

    #region Unity API Functions
    void Start () {
        paceKeyText = this.GetComponent<Text>();
        GameManager.Instance.CyclePaceKey.AddListener(UpdatePaceKey);
        UpdatePaceKey();
	}
    #endregion

    #region Text Manipulation Functions
    /// <summary>
    /// Updates the UI text for the next key the player needs to hit in order to keep a steady pace.
    /// </summary>
    private void UpdatePaceKey()
    {
        paceKeyText.text = GameManager.Instance.currentPaceKey;
    }
    #endregion
}
