using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationController : MonoBehaviour {

    #region Variables
    private Animator anim;
    #endregion

    #region Unity API Functions
    void Start () {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
        GameManager.Instance.MinigameStart.AddListener(PlayRunAnimation);
	}
    #endregion

    #region Animation Functions
    /// <summary>
    /// Plays the run animation as soon as the minigame begins.
    /// </summary>
    private void PlayRunAnimation()
    {
        anim.enabled = true;
    }
    #endregion
}
