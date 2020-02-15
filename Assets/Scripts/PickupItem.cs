using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupItem : MonoBehaviour
{
	[SerializeField]
	private bool pickedUp = false;

	[SerializeField]
	private List<AudioClip> successClips;

	[SerializeField]
	private List<AudioClip> pickupClips;

	private AudioSource source;

	public void Start()
	{
		source = GetComponent<AudioSource>();
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !pickedUp)
		{
            // stop pickup sound playing
            source.Stop();
            source.volume = 1;
			// reparent to other.collider.transform?
			transform.SetParent(other.transform, false);
            transform.localPosition = new Vector3(0, 0, 0);

			pickedUp = true;

			if (pickupClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, pickupClips.Count - 1);
				source.PlayOneShot(pickupClips[clipIndex], 1f);
			}
		}
	}

	//private IEnumerator DeactivateSelfAfterDelay(float seconds)
	//{
	//	yield return new WaitForSeconds(seconds);
	//	transform.parent.gameObject.SetActive(false);
	//}
}
