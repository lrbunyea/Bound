using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour {

    #region Unity API Functions
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.FadeInPanel();
        GameManager.Instance.DisablePlayerMovement();
        GameManager.Instance.PrimingBlackScreen.Invoke();
    }
    #endregion
}
