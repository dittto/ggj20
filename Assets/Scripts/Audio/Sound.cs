using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.75f;

    [Range(0.1f, 3f)]
    public float pitch = 1;

    public bool loop;
    public bool isMusic;

    [HideInInspector]
    public AudioSource source;
}
