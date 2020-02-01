using System;
using UnityEngine;

/// <summary>
/// General UI Event Args
/// </summary>
public class UIEventArgsBase : EventArgs
{
    // Basically, source is only useful to check stuff if casted to some specific type
    public object source { get; }
    public Type target { get; }

    protected UIEventArgsBase() { }
    public UIEventArgsBase( Type targetType, object sourceObj )
    {
        if( targetType != null )
        {
            target = targetType;
        }

        if( sourceObj != null )
        {
            source = sourceObj;
        }
    }
}

// FIXME: LH: technically bad practice to have multiple classes in same file in C# land


/// <summary>
/// Audio event type differentiation
/// </summary>
public enum AUDIO_EVENT_TYPE
{
    PLAY_AUDIO = 0,
    FADE_IN_AUDIO = 1,
    FADE_OUT_AUDIO = 2
}

/// <summary>
/// Custom UI Audio Event Args
/// </summary>
public class UIAudioEventArgs : UIEventArgsBase
{
    public AUDIO_EVENT_TYPE audioEventType { get; }
    public AudioClip clip { get; }

    public UIAudioEventArgs( Type targetType, object sourceObj, AUDIO_EVENT_TYPE eventType, AudioClip newClip ) : base( targetType, sourceObj )
    {
        audioEventType = eventType;
        clip = newClip;
    }
}

/// <summary>
/// Custom UI Animation Event Args
/// </summary>
public class UIAnimationEventArgs : UIEventArgsBase
{
	public Animator animatorRef { get; }

    public String animationID { get; }

    public UIAnimationEventArgs(Type targetType, object sourceObj, Animator animator, String clipID ) : base(targetType, sourceObj)
    {
		animatorRef = animator;
		animationID = clipID;
    }
}