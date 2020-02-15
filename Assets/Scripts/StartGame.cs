﻿using System;
using System.Collections;
using UnityEngine;

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
	private AudioClip explorationMusic;

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
            //eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.PLAY_AUDIO, startMenuMusic));
        }

		animator = GetComponent<Animator>();

		startPressed = false;
    }

    private void Update()
    {
        if (Input.anyKey && !startPressed)
        {
            // FIXME: LH: find way to stop clip from playing when this is done; Non-essential, as atm the scene ending stops it
            if( eventForwarder != null && animator)
            {
				eventForwarder.EnqueueEvent(new UIAnimationEventArgs(typeof(UIAnimationManager), this.GetType(), animator, transitionParameter));
                eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_OUT_AUDIO, null));

				StartCoroutine(StartLoopMusicAfterDelay(8));
			}

            StartCoroutine(PanCameraAfterAnimationEnd(clip.length));
			startPressed = true;
        }


    }

    private IEnumerator PanCameraAfterAnimationEnd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(IntroCameraPan), this.GetType()));
	}

	private IEnumerator StartLoopMusicAfterDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.STOP_AUDIO, null));
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_IN_AUDIO, null));
		//eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.PLAY_AUDIO, explorationMusic));
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.ENABLE_AUDIO_LOOPING, null));
	}
}
