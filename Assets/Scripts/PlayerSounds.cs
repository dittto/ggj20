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

	public void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
		{
			if( sadClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, sadClips.Count - 1);
				_source.PlayOneShot(sadClips[clipIndex]);
        
				_playerAnimator.SetTrigger("Terrified");
			}
		}

		else if(other.collider.CompareTag("Panel") && 
				!GetComponentInChildren<PickupItem>())
		{
			if (sadClips.Count > 0)
			{
				System.Random rng = new System.Random();
				int clipIndex = rng.Next(0, sadClips.Count - 1);
				_source.PlayOneShot(sadClips[clipIndex]);
        
				_playerAnimator.SetTrigger("Terrified");
			}
		}
        
        else if (other.collider.gameObject.layer == 9) {
	        if (sadClips.Count > 0)
	        {
		        System.Random rng = new System.Random();
		        int clipIndex = rng.Next(0, sadClips.Count - 1);
		        _source.PlayOneShot(sadClips[clipIndex]);
        
		        _playerAnimator.SetTrigger("Terrified");
	        }
        }
    }
}