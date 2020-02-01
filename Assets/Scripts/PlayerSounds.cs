using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    private AudioSource _source;

    public void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player")) {
            _source.Play();
        }
    }
}