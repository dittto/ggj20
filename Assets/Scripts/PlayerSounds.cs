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

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            Debug.Log("test");
            _source.Play();
        }
    }
}
