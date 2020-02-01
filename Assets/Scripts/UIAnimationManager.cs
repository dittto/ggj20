using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// FIXME: LH:   in the name of simplifying this basic work atm - UI will have no 
//              cooldowns on animations (as so far 99% of the time they only play once for transitions etc.)
//              Inherit InputCooldownBase if in need of cooldown handling

public class UIAnimationManager : MonoBehaviour, IPlayAnimations
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        UIEventForwarder.OnForwardedEvent += ForwardedEventHandler;
    }

    private void Update()
    {
    }

    // FIXME: LH: copy-pasting, can probably be generified in some way
    private void ForwardedEventHandler( UIEventArgsBase args )
    {
        if( args != null )
        {
            UIAnimationEventArgs animationArgs = args as UIAnimationEventArgs;
            if ( animationArgs != null )
            {
                Type targetType = animationArgs.target;
                if ( targetType == this.GetType() )
                {
                    PlayAnimation( animationArgs.animatorRef, animationArgs.animationID );
                }
            }
        }
    }

    public bool PlayAnimation( Animator animatorRef, System.String animationID )
    {
        if (animationID != String.Empty)
        {
            Debug.Log("Playing animation clip/transition: " + animationID);

            if( animatorRef ) 
            {
				Debug.Log("Animator != null");
				Animator.StringToHash(animationID);
				animatorRef.SetBool(animationID, true );
                return true;
            }
        }

        return false;
    }
}
