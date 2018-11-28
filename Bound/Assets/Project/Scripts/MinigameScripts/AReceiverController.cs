using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AReceiverController : MonoBehaviour {

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
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (isValid)
                {
                    GameManager.Instance.CorrectKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectBreatheKeyPressed("A");
                } else
                {
                    GameManager.Instance.IncorrectKeyPress.Invoke();
                    AnalyticsManager.Instance.IncorrectBreatheKeyPressed("A");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PromptController>().isColliding = true;
        isValid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PromptController>().isColliding = false;
        isValid = false;
    }
    #endregion
}
