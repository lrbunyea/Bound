﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour {

    #region Unity API Functions
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.MinigameStart.Invoke();
    }
    #endregion
}