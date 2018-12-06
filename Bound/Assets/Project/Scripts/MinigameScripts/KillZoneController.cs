using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillZoneController : MonoBehaviour {

    #region Unity API Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ConPenalty.Invoke();
        AnalyticsManager.Instance.KeyPassed(collision.gameObject.GetComponent<Text>().text);
        Destroy(collision.gameObject);
    }
    #endregion
}
