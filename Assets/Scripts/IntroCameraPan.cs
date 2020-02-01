using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IntroCameraPan : MonoBehaviour
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
	private Animator animator;

	[SerializeField]
	private bool panDone = false;


	void Start()
    {
		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if (eventForwarder)
		{
			UIEventForwarder.OnForwardedEvent += StartPanEventHandler;
		}

		animator = GetComponent<Animator>();

		panDone = false;
	}

    void Update()
    {
		// FIXME: LH: maybe check if we're at the right position 
		// to set panDone to true
    }

	private void StartPanEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				StartCoroutine(RemoveOverlayAndActivatePlayers(clip.length));
			}
		}
	}

	public bool PlayAnimation(Animator animatorRef, System.String animationID)
	{
		if (animationID != String.Empty)
		{
			Debug.Log("Playing animation clip/transition: " + animationID);

			if (animatorRef)
			{
				Debug.Log("Animator != null");

				Animator.StringToHash(animationID);
				animatorRef.SetBool(animationID, true);
				return true;
			}
		}

		return false;
	}

	private IEnumerator RemoveOverlayAndActivatePlayers(float seconds)
	{
		PlayAnimation(animator, "isPanStarted");
		yield return new WaitForSeconds(seconds);

		eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(DissolveShipOverlay), this.GetType()));
		eventForwarder.EnqueueEvent(new UIEventArgsBase(typeof(ActivatePlayers), this.GetType()));
	}
}
