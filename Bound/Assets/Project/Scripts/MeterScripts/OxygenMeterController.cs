﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeterController : MonoBehaviour {

    #region Variables
    private Slider oxygenSlider;
    #endregion

    #region Unity API Functions
    private void Awake()
    {
        oxygenSlider = this.GetComponent<Slider>();
    }

    void Start()
    {
        oxygenSlider.value = GameManager.Instance.currentOxygen;
    }

    void Update()
    {
        oxygenSlider.value = GameManager.Instance.currentOxygen;
    }
    #endregion
}