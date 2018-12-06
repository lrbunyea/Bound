using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WReceiverController : MonoBehaviour {

    #region Variables
    private bool isValid;
    #endregion

    #region Unity API Functions
    void Start()
    {
        isValid = false;
    }

    void Update()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Minigame)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (isValid)
                {
                    GameManager.Instance.CorrectWKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectKeyPressed("W");
                } else
                {
                    GameManager.Instance.ConPenalty.Invoke();
                    AnalyticsManager.Instance.IncorrectKeyPressed("W");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<WPromptController>().isColliding = true;
        isValid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<WPromptController>().isColliding = false;
        isValid = false;
    }
    #endregion
}
