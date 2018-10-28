using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheKeyController : MonoBehaviour {

    #region Variables
    Text breatheKeyText;
    #endregion

    #region Unity API Functions
    void Start()
    {
        breatheKeyText = this.GetComponent<Text>();
        GameManager.Instance.CycleBreatheKey.AddListener(UpdateBreatheKey);
        UpdateBreatheKey();
    }
    #endregion

    #region Text Manipulation Functions
    /// <summary>
    /// Updates the UI text for the next key the player needs to hit in order to stop and breathe during their run.
    /// </summary>
    private void UpdateBreatheKey()
    {
        breatheKeyText.text = GameManager.Instance.currentBreatheKey;
    }
    #endregion
}
