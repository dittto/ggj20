using System;
using System.Collections;
using UnityEngine;

public class EndGame : MonoBehaviour
{
	[SerializeField]
	private GameObject EndGameCanvas;
 
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
	private Animator animator;

	[SerializeField]
	private AudioClip endMusic;

	[SerializeField]
	private bool isEnding;

	[SerializeField]
	private GameObject player1;

	[SerializeField]
	private GameObject player2;

	[SerializeField]
	private GameObject rope;

	[SerializeField]
	private KeyCode _quitGameKeyCode;

	void Start()
	{
		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if (eventForwarder)
		{
			UIEventForwarder.OnForwardedEvent += EndGameEventHandler;
		}

		isEnding = false;
	}


	private void EndGameEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				EndGameCanvas.SetActive(true);
			}
		}
	}

	public void StartEnd()
	{
		if( !isEnding )
		{
			eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_OUT_AUDIO, null));

			//EndGameCanvas.SetActive(true);
			StartCoroutine(StartEndingAfterDelay(6));

			isEnding = true;
		}

		if (isEnding)
		{
			if (Input.GetKeyUp(_quitGameKeyCode))
			{
				Application.Quit();
			}
		}
	}

	private IEnumerator StartEndingAfterDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);

		player1.SetActive(false);
		player2.SetActive(false);
		rope.SetActive(false);

		// FIXME: start animation sequence here
		EndGameCanvas.SetActive(true);
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.STOP_AUDIO, null));
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.FADE_IN_AUDIO, null));
		eventForwarder.EnqueueEvent(new UIAudioEventArgs(typeof(UIAudioManager), this.GetType(), AUDIO_EVENT_TYPE.PLAY_AUDIO, endMusic));
	}
}
