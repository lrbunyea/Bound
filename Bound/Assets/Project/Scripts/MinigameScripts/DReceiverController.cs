using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DReceiverController : MonoBehaviour {

    #region Variables
    private bool isValid;
    private Image image;
    private RectTransform rect;
    #endregion

    #region Unity API Functions
    void Start()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
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
                    GoodKeyPressAnim();
                    GameManager.Instance.CorrectDKeyPress.Invoke();
                    AnalyticsManager.Instance.CorrectKeyPressed("D");
                } else
                {
                    BadKeyPressAnim();
                    GameManager.Instance.ConPenalty.Invoke();
                    AnalyticsManager.Instance.IncorrectKeyPressed("D");
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                ReturnToOrigin();
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

    //All of these functions just modify the scale and color of the reciever sprite
    //on good/ bad key presses
    #region Animation Functions
    private void GoodKeyPressAnim()
    {
        image.color = new Color(0, 1, 1, .5f); //cyan
        rect.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    private void BadKeyPressAnim()
    {
        image.color = new Color(1, 0, 0, .5f); //red
        rect.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    private void ReturnToOrigin()
    {
        image.color = new Color(1, 1, 1, .5f); //white
        rect.localScale = new Vector3(1f, 1f, 1f);
    }
    #endregion
}
