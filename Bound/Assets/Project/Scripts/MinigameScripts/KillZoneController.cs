using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour {

    #region Unity API Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.IncorrectKeyPress.Invoke();
        Destroy(collision.gameObject);
    }
    #endregion
}
