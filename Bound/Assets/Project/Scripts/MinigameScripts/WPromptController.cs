using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPromptController : MonoBehaviour {

    #region Variables
    public bool isColliding;
    #endregion

    #region Unity API Functions
    void Start()
    {
        isColliding = false;
        GameManager.Instance.CorrectWKeyPress.AddListener(DeletePrompt);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, GameManager.Instance.currentSpeed * GameManager.Instance.currentSpeedMult, 0));
    }
    #endregion

    /// <summary>
    /// A function that is invoked when the button is pressed ony when the prompt enters the zone.
    /// </summary>
    private void DeletePrompt()
    {
        if (isColliding)
        {
            Destroy(this.gameObject);
        }
    }
}
