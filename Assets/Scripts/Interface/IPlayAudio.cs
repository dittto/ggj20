using UnityEngine;

public interface IPlayAudio
{
    /// <summary>
    /// Generic audio playing function
    /// </summary>
    /// <param name="clip">Audio Clip</param>
    /// <returns>success/fail status</returns>
    bool PlayAudio( AudioClip clip );
}
