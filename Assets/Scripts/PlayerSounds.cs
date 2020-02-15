using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
	public Animator _playerAnimator;
	
    private AudioSource _source;

	[SerializeField]
	private List<AudioClip> cheerfulClips;

	[SerializeField]
	private List<AudioClip> sadClips;

	[SerializeField]
	private List<AudioClip> interpersonCollisionClips;

	[SerializeField]
	private List<AudioClip> environmentCollisionClips;

	public void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
		{
			if( sadClips.Count > 0 && interpersonCollisionClips.Count > 0 )
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, sadClips.Count - 1);
				_source.PlayOneShot(sadClips[clipIndex]);

				int clipIndex2 = rng.Next(0, interpersonCollisionClips.Count - 1);
				_source.PlayOneShot(interpersonCollisionClips[clipIndex2]);

				_playerAnimator.SetTrigger("Terrified");
			}
		}
        else if (other.collider.gameObject.layer == 9)
        {
            System.Random rng = new System.Random();
            if (sadClips.Count > 0)
            {
                int clipIndex = rng.Next(0, sadClips.Count - 1);
                _source.PlayOneShot(sadClips[clipIndex]);
            }
            if (environmentCollisionClips.Count > 0) {
                int clipIndex2 = rng.Next(0, environmentCollisionClips.Count - 1);
                _source.PlayOneShot(environmentCollisionClips[clipIndex2]);
            }

            _playerAnimator.SetTrigger("Terrified");
        }
    }
}