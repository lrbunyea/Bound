using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DReceiverController : MonoBehaviour {

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
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (isValid)
                {
                    GameManager.Instance.CorrectDKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectBreatheKeyPressed("D");
                } else
                {
                    GameManager.Instance.IncorrectKeyPress.Invoke();
                    AnalyticsManager.Instance.IncorrectBreatheKeyPressed("D");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<DPromptController>().isColliding = true;
        isValid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<DPromptController>().isColliding = false;
        isValid = false;
    }
    #endregion
}
