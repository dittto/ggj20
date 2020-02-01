using UnityEngine;

public interface IHoldAudioClips
{
    //FIXME: LH: probably need some sort of indexing system if it's gonna be this generic?

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Held Audio Clip</returns>
    AudioClip GetAudioClip();
}
