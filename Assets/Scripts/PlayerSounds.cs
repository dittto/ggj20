using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    private AudioSource _source;

	// private List<AudioClip> cheerfulClips;
	// private List<AudioClip> sadClips;

	public void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player")) {
            _source.Play();

			// FIXME: LH: change to playOneShot( random selection from either list )
        }
    }
}