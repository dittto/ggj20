﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// FIXME: LH: this will need extending w/ events so it can play the other menu sounds
// - Also, pending resolution of how to make it transcend scenes, so can be used in all menus
[RequireComponent(typeof(AudioSource))]
public class UIAudioManager : MonoBehaviour, IPlayAudio
{
    private AudioSource audioSource;

    // FIXME: LH: this will need to change when extending- currently uses only one mixer track, but
    // might be good to have e.g. music and SFX on different tracks
    [SerializeField]
    private AudioMixer audioMixer;

    // Fade parameters
    [SerializeField]
    private float musicFadeInDuration;

    [SerializeField]
    private float musicFadeOutDuration;

    [SerializeField]
    private String mixerTrackName;

	[SerializeField]
	private AudioClip explorationClip;

	[SerializeField]
	private AudioClip outroClip;

	private IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;

        float currentVol = 0;
        bool result = audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

	private IEnumerator StartBasicFade(AudioSource audioSource, float duration, float targetVolume)
	{
		float currentTime = 0;
		float start = audioSource.volume;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
			yield return null;
		}
		yield break;
	}

	private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        UIEventForwarder.OnForwardedEvent += ForwardedEventHandler;
    }

    private void ForwardedEventHandler( UIEventArgsBase args )
    {
        if ( args != null )
        {
            UIAudioEventArgs audioArgs = args as UIAudioEventArgs;
            if ( audioArgs != null )
            {
                Type targetType = audioArgs.target;
                if ( targetType == this.GetType() )
                {
                    switch( audioArgs.audioEventType )
                    {
                        case AUDIO_EVENT_TYPE.PLAY_AUDIO:
                            PlayAudio(audioArgs.clip);
                            break;

						case AUDIO_EVENT_TYPE.FADE_IN_AUDIO:
                            StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeInDuration, 1));
                            break;

                        case AUDIO_EVENT_TYPE.FADE_OUT_AUDIO:
							StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeOutDuration, -1));
							//StartCoroutine(StartBasicFade(audioSource, musicFadeOutDuration, -80));
							break;

						case AUDIO_EVENT_TYPE.FADE_IN_EXPLORATION_AUDIO:
							//StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeOutDuration, -1));
							StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeInDuration, 1));
							PlayAudio(explorationClip);
							break;

						case AUDIO_EVENT_TYPE.FADE_IN_OUTRO_AUDIO:
							//StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeOutDuration, -1));
							StartCoroutine(StartFade(audioMixer, mixerTrackName, musicFadeInDuration, 1));
							PlayAudio(outroClip);
							break;

						default:
                            break;
                    }

                }
            }
        }
    }

    // FIXME: LH: need overrides for this case
    public bool PlayAudio( AudioClip clip )
    {
        if ( clip != null )
        { 
            audioSource.PlayOneShot( clip );
            return true;
        }

        return false;
    }
}
