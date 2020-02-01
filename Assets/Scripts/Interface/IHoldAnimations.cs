using System;

public interface IHoldAnimations
{
    //FIXME: LH: probably need some sort of indexing system if it's gonna be this generic?

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <returns>Held Audio Clip</returns>
    //Animation GetAnimation();

    /// <summary>
    /// The Animations are indexed by string ID/state, stored on Animator, 
    /// so this is the only way (seemingly) to attach them to each weapon class.
    /// </summary>
    /// <returns>Held Audio Clip</returns>
    String GetAnimationID();
}
