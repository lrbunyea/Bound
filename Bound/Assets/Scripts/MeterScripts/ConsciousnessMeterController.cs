﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsciousnessMeterController : MonoBehaviour {

    #region Variables
    private Slider conSlider;
    #endregion

    #region Unity API Functions
    private void Awake()
    {
        conSlider = this.GetComponent<Slider>();
    }

    void Start()
    {
        conSlider.value = 100;
    }

    void Update()
    {

    }
    #endregion
}
