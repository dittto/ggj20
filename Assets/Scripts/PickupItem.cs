using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupItem : MonoBehaviour
{
	[SerializeField]
	private bool pickedUp = false;

	[SerializeField]
	private Transform parentTransform = null;

	[SerializeField]
	private List<AudioClip> successClips;

	[SerializeField]
	private List<AudioClip> pickupClips;

	private AudioSource source;

	public void Start()
	{
		source = GetComponent<AudioSource>();
		parentTransform = transform.parent.GetComponentInParent<Transform>();
	}

	public void Update()
	{
		// FIXME: LH: I kinda hate this but oh well
		if( pickedUp )
		{
			parentTransform.position = parentTransform.parent.position;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !pickedUp)
		{
			// stop pickup sound playing
			GetComponent<AudioSource>().Stop();

			// reparent to other.collider.transform?
			parentTransform.SetParent(other.transform, false);

			pickedUp = true;

			if (successClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, successClips.Count - 1);
				source.PlayOneShot(successClips[clipIndex]);
			}

			if (pickupClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, pickupClips.Count - 1);
				source.PlayOneShot(pickupClips[clipIndex]);
			}
		}
		else if( other.CompareTag("Panel"))
		{
			if (successClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, successClips.Count - 1);
				source.PlayOneShot(successClips[clipIndex]);
			}

			DeactivateSelfAfterDelay(1);
		}
	}

	private IEnumerator DeactivateSelfAfterDelay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		transform.parent.gameObject.SetActive(false);
	}
}
