using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    #region Variables
    public static SoundManager Instance;

    //references to all audio sources
    [SerializeField] AudioSource mainMusic;
    [SerializeField] AudioSource hospital;
    [SerializeField] AudioSource car;
    [SerializeField] AudioSource running;
    [SerializeField] AudioSource regularBreathing;
    [SerializeField] AudioSource sharpBreath;
    [SerializeField] AudioSource dialogue;

    //references to dialogue audio clips by location
    [SerializeField] AudioClip[] dialogueClips;

    public float carClipLength;
    private float breathingTimer;
    private float timeLeft;
    private bool hospitalFade;
    private bool musicFade;
    private bool runningFade;
    private bool breathingFade;
    private bool sharpBreathPlaying;
    private int dcIndex;
    #endregion

    private void Awake()
    {
        //Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        dcIndex = -1;
        carClipLength = car.clip.length;
        breathingTimer = sharpBreath.clip.length;
        timeLeft = breathingTimer;
        hospitalFade = false;
        musicFade = false;
        runningFade = false;
        breathingFade = false;
        sharpBreathPlaying = false;

        //register functions to events
        GameManager.Instance.ConPenalty.AddListener(TakeSharpBreath);
        GameManager.Instance.ConPenalty.AddListener(SlowRunning);
        GameManager.Instance.MinigameStart.AddListener(PlayRunning);
        GameManager.Instance.MinigameStart.AddListener(PlayBreathing);
        GameManager.Instance.StartBlackScreen.AddListener(PlayHospital);
        GameManager.Instance.EndBlackScreen.AddListener(FadeOutBreathing);
        GameManager.Instance.EndBlackScreen.AddListener(FadeOutRunning);
        GameManager.Instance.EndBlackScreen.AddListener(FadeOutMusic);
        GameManager.Instance.EndBlackScreen.AddListener(PlayHospital);
    }
	
	// Update is called once per frame
	void Update () {
        //Hospital crossfade
        if (hospitalFade)
        {
            hospital.volume -= Time.deltaTime;
            if (hospital.volume <= 0)
            {
                hospital.Stop();
                hospitalFade = false;
            }
        }
        //Music crossfade
        if (musicFade)
        {
            mainMusic.volume -= Time.deltaTime;
            if (mainMusic.volume <= 0)
            {
                mainMusic.Stop();
                musicFade = false;
            }
        }
        //Running crossfade
        if (runningFade)
        {
            running.volume -= Time.deltaTime;
            if (running.volume <= 0)
            {
                running.Stop();
                runningFade = false;
            }
        }
        //Breathing crossfade
        if (breathingFade)
        {
            regularBreathing.volume -= Time.deltaTime;
            if (regularBreathing.volume <= 0)
            {
                regularBreathing.Stop();
                breathingFade = false;
            }
        }
        //Sharp breathing timer
        if(sharpBreathPlaying)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = breathingTimer;
                sharpBreath.Stop();
                sharpBreathPlaying = false;
                if (GameManager.Instance.currentConsciousness != 0)
                {
                    regularBreathing.Play();
                }
            }
        }
    }

    #region Helper Functions
    /// <summary>
    /// Function that is called when the player presses an incorrect key.
    /// </summary>
    private void TakeSharpBreath()
    {
        regularBreathing.Stop();
        sharpBreath.Play();
        sharpBreathPlaying = true;
    }

    /// <summary>
    /// Plays the next line of dialogue when called
    /// </summary>
    public void SwitchDialogueClip()
    {
        dialogue.Stop();
        dcIndex++;
        if (dcIndex == dialogueClips.Length)
        {
            return;
        } else
        {
            dialogue.clip = dialogueClips[dcIndex];
            dialogue.Play();
        }
    }

    /// <summary>
    /// Pitches down and also slows the running sound effects when a player presses a key incorrectly.
    /// </summary>
    private void SlowRunning()
    {
        running.pitch -= .1f;
    }

    /// <summary>
    /// Helps the dialogue text controller know how long to display line text.
    /// </summary>
    /// <returns>The length, in seconds, of the dialogue clip that is currently playing</returns>
    public float CurrentLineTime()
    {
        return dialogue.clip.length;
    }
    #endregion

    #region Play Functions
    /// <summary>
    /// Plays the hospital sound audio source.
    /// </summary>
    public void PlayHospital()
    {
        hospital.volume = .2f;
        hospital.Play();
    }

    /// <summary>
    /// Plays the car sound audio source.
    /// </summary>
    public void PlayCar()
    {
        car.Play();
    }

    /// <summary>
    /// Plays the music audio source.
    /// </summary>
    public void PlayMusic()
    {
        mainMusic.Play();
    }

    /// <summary>
    /// Plays the running sound udio source.
    /// </summary>
    public void PlayRunning()
    {
        running.Play();
    }

    /// <summary>
    /// Plays the breathing sound audio source.
    /// </summary>
    public void PlayBreathing()
    {
        regularBreathing.Play();
    }

    /// <summary>
    /// Plays the dialogue audio source.
    /// </summary>
    public void PlayDialogue()
    {
        dialogue.Play();
    }
    #endregion

    #region Fade Out Functions
    //All these functions switch the flag on.
    public void FadeOutHospital()
    {
        hospitalFade = true;
    }

    public void FadeOutMusic()
    {
        musicFade = true;
    }

    public void FadeOutRunning()
    {
        runningFade = true;
    }

    public void FadeOutBreathing()
    {
        breathingFade = true;
    }
    #endregion
}
