using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreatheCooldownTextController : MonoBehaviour {

    #region Variables
    Text cdText;
    [SerializeField] float cooldownTimer;
    private float timeLeft;
    #endregion

    #region Unity API Functions
    void Start () {
        cdText = this.GetComponent<Text>();
        timeLeft = cooldownTimer;
	}
	
	void Update () {
		if (GameManager.Instance.isBreatheCooldownActive)
        {
            //Counts down cooldownTimer number of seconds, update text field accordingly
            timeLeft -= Time.deltaTime;
            cdText.text = "Cooldown: " + timeLeft;
            if (timeLeft < 0)
            {
                GameManager.Instance.SetBreatheCooldownInactive();
                timeLeft = cooldownTimer;
                cdText.text = "";
            }
        }
	}
    #endregion
}
