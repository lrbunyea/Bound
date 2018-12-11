using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimationController : MonoBehaviour {

    #region Variables
    private Animator anim;
    public bool walkingUp;
    public bool isRotating;
    [SerializeField] float speed;
    [SerializeField] Vector3 target;
    private float animSpeed;
    private bool isSlow;
    #endregion

    #region Unity API Functions
    void Start () {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
        walkingUp = false;
        isRotating = false;
        isSlow = false;
        animSpeed = .7f;
        target = new Vector3(150.15f, 9.1f, 182.21f);
        GameManager.Instance.MinigameStart.AddListener(PlayRunAnimation);
        GameManager.Instance.ConPenalty.AddListener(SlowDown);
	}

    private void Update()
    {
        if (walkingUp)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (transform.position == target)
            {
                walkingUp = false;
                isRotating = true;
                
            }
        }

        if (isRotating)
        {
            transform.Rotate(new Vector3(0, -1, 0));
            if(Mathf.Round(transform.eulerAngles.y) == 270)
            {
                isRotating = false;
                GameManager.Instance.MinigameStart.Invoke();
            }
        }

        if (isSlow)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("running"))
            {
                anim.speed = animSpeed;
                isSlow = false;
            }
        }
    }
    #endregion

    #region Animation Functions
    /// <summary>
    /// Plays the run animation as soon as the minigame begins.
    /// </summary>
    private void PlayRunAnimation()
    {
        anim.enabled = true;
    }

    /// <summary>
    /// Signals to the controller to slow down the speed of the running animation
    /// </summary>
    private void SlowDown()
    {
        isSlow = true;
        animSpeed -= .1f;
    }
    #endregion
}
