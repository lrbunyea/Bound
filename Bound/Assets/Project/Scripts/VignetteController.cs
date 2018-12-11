using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class VignetteController : MonoBehaviour {

    #region Variables
    [SerializeField] PostProcessingProfile ppp;
    private VignetteModel.Settings vignetteSettings;
    private float newIntensity;
    private float currentIntensity;
    private bool isLerping;
    private int lerpCount;
    #endregion

    #region Unity API Functions
    void Start () {
        ppp = GetComponent<PostProcessingBehaviour>().profile;
        vignetteSettings = ppp.vignette.settings;
        newIntensity = ppp.vignette.settings.intensity;
        currentIntensity = ppp.vignette.settings.intensity;
        lerpCount = 0;
        GameManager.Instance.ConPenalty.AddListener(PassOut);
	}

    private void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Minigame)
        {
            if (isLerping)
            {
                currentIntensity = Mathf.Lerp(ppp.vignette.settings.intensity, newIntensity, .1f);
                vignetteSettings.intensity = currentIntensity;
                ppp.vignette.settings = vignetteSettings;
                lerpCount++;
                //Need to turn off the lerp after it's finished (The way I'm doing this is kind of bad)
                if (lerpCount == 200)
                {
                    isLerping = false;
                    lerpCount = 0;
                }
            }
        }
    }
    #endregion

    #region Event Functions
    /// <summary>
    /// Function that listens for an event that is triggered when the player incorrectly pressed a key
    /// Increases the size of the vignette on the screen
    /// </summary>
    private void PassOut()
    {
        isLerping = true;
        newIntensity += .2f;
    }
    #endregion
}
