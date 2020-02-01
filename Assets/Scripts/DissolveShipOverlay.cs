using System;
using UnityEngine;

public class DissolveShipOverlay : MonoBehaviour
{
	[SerializeField]
	private GameObject eventUtilsPrefab;

	[SerializeField]
	private UIEventForwarder eventForwarder;

	[SerializeField]
	private Animator animator;


	void Start()
	{
		if (eventUtilsPrefab)
		{
			eventForwarder = eventUtilsPrefab.GetComponent<UIEventForwarder>();
		}

		if (eventForwarder)
		{
			UIEventForwarder.OnForwardedEvent += DissolveShipOverlayEventHandler;
		}

		animator = GetComponent<Animator>();
	}

	void Update()
	{
	}

	private void DissolveShipOverlayEventHandler(UIEventArgsBase args)
	{
		if (args != null)
		{
			Type targetType = args.target;
			if (targetType == this.GetType())
			{
				PlayAnimation(animator, "dissolveOverlay");
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
}
