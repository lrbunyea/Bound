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
                    GameManager.Instance.CorrectAKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectKeyPressed("A");
                } else
                {
                    GameManager.Instance.ConPenalty.Invoke();
                    AnalyticsManager.Instance.IncorrectKeyPressed("A");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<APromptController>().isColliding = true;
        isValid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<APromptController>().isColliding = false;
        isValid = false;
    }
    #endregion
}
