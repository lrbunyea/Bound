using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SReceiverController : MonoBehaviour {

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
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (isValid)
                {
                    GameManager.Instance.CorrectSKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectKeyPressed("S");
                } else
                {
                    GameManager.Instance.ConPenalty.Invoke();
                    AnalyticsManager.Instance.IncorrectKeyPressed("S");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SPromptController>().isColliding = true;
        isValid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SPromptController>().isColliding = false;
        isValid = false;
    }
    #endregion
}
