﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeterController : MonoBehaviour {

    #region Variables
    private Slider speedSlider;
    #endregion

    #region Unity API Functions
    private void Awake()
    {
        speedSlider = this.GetComponent<Slider>();
    }

    void Start () {
        speedSlider.value = 0;
	}
	
	void Update () {
		
	}
    #endregion
}
