using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject eventUtilsPrefab;

    [SerializeField]
    private UIEventForwarder eventForwarder;

    // Note:
    // Literally so we just get the exact length of the animation;
    // There is another way of getting a specific clip from the animator
    // but it requires iterating over all member clips. 
    // Pending me finding some solution; Perhaps I eventually integrate this in the base class :D
    [SerializeField]
    private AnimationClip clip;

	[SerializeField]
	private String transitionParameter;

	[SerializeField]
	private Animator animator;

	[SerializeField]
    private AudioClip startMenuMusic;

	[SerializeField]
	private bool startPressed = false;

    private void Start()
    {
        if( eventUtilsPrefab )
        {
            eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
        }

        if( eventForwarder )
        {
            eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_IN_AUDIO, null));
            eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.PLAY_AUDIO, startMenuMusic));
        }

		animator = GetComponent<Animator>();

		startPressed = false;
    }

    private void Update()
    {
        if (Input.GetButton("Submit") && !startPressed)
        {
			Debug.Log("Submit pressed.");

            // FIXME: LH: find way to stop clip from playing when this is done; Non-essential, as atm the scene ending stops it
            if( eventForwarder != null && animator)
            {
				Debug.Log("EventForwarder != null");

				eventForwarder.EnqueueEvent(new UIAnimationEventArgs(typeof(UIAnimationManager), this.GetType(), animator, transitionParameter));
                eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_OUT_AUDIO, null));
            }

            StartCoroutine(StartGameAfterAnimationEnd(clip.length));
			startPressed = true;
        }
    }

    private IEnumerator StartGameAfterAnimationEnd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //SceneManager.LoadScene("LoadoutScreen");

		// FIXME: LH:	play pan animation,
		//				activate players
    }
}
