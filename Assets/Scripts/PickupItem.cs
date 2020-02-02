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

	public void Start()
	{
		parentTransform = transform.parent.GetComponentInParent<Transform>();
	}

	public void Update()
	{
		// I kinda hate this but oh well
		if( pickedUp )
		{
			// FIXME: LH: can at least cache this
			parentTransform.position = parentTransform.parent.position;
				//new Vector3( parentTransform.parent.position.x,
				//			parentTransform.parent.position.y,
				//			0);
		}
	}

	//public void OnCollisionEnter(Collision other)
	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !pickedUp)
		{
			// stop pickup sound playing
			GetComponent<AudioSource>().Stop();

			// reparent to other.collider.transform?
			parentTransform.SetParent(other.transform, false);

			pickedUp = true;
		}
		else if( other.CompareTag("Panel"))
		{
			transform.parent.gameObject.SetActive(false);

			// FIXME: LH: play celebratory gong
		}
	}
}
