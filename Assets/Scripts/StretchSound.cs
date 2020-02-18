using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchSound : MonoBehaviour
{
	[SerializeField]
	private GameObject player1;

	[SerializeField]
	private GameObject player2;

	[SerializeField]
	private AudioSource source;

	[SerializeField]
	private List<AudioClip> stretchingClips;

	[SerializeField]
	private float cooldown;

	private float nextTime;

	private void Start()
	{
		source = GetComponent<AudioSource>();

		nextTime = 0;
	}

	private void Update()
    {
        if( player1 && player2 )
		{
			if( Vector3.Distance( player1.transform.position, player2.transform.position ) > 15 && 
				Time.time > nextTime )
			{
				if ( stretchingClips.Count > 0 )
				{ 
					System.Random rng = new System.Random();
					int clipIndex = rng.Next(0, stretchingClips.Count - 1);
					source.PlayOneShot(stretchingClips[clipIndex]);

					nextTime = Time.time + cooldown;
				}
			}
		}
    }
}
