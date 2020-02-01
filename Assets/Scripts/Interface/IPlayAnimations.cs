using UnityEngine;

public interface IPlayAnimations
{
    /// <summary>
    /// Generic animation playing function
    /// </summary>
    /// <param name="animation">Animation</param>
    /// <returns>success/fail status</returns>
    bool PlayAnimation( Animator animatorRef, System.String animationID );
}
