using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VignetteController : MonoBehaviour {

    #region Variables
    [SerializeField] PostProcessingProfile ppp;
    #endregion

    #region Unity API Functions
    void Start () {
        ppp = GetComponent<PostProcessingBehaviour>().profile;
        GameManager.Instance.IncorrectKeyPress.AddListener(PassOut);
	}
    #endregion

    #region Event Functions
    /// <summary>
    /// Function that listens for an event that is triggered when the player incorrectly pressed a key
    /// Increases the size of the vignette on the screen
    /// </summary>
    private void PassOut()
    {
        VignetteModel.Settings vignetteSettings = ppp.vignette.settings;
        vignetteSettings.intensity = ppp.vignette.settings.intensity + .2f;
        ppp.vignette.settings = vignetteSettings;
    }
    #endregion
}
