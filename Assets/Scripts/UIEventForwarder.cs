using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event Forwarding service for UI Events, so that specific objects can use it as 
/// an intermediate step to send events without having to directly couple to other objects within the scene.
/// </summary>
public class UIEventForwarder : MonoBehaviour
{
    private Queue<UIEventArgsBase> eventQueue;

    public delegate void UIEventHandler( UIEventArgsBase args );
    public static event UIEventHandler OnForwardedEvent;

    private void Start()
    {
        // FIXME: LH: this is a bit of a hack, but issue due to timing-
        // this start might not always be called before other Starts(), who want
        // to already enqueue. So we do it twice?
        if( eventQueue == null )
        { 
            eventQueue = new Queue<UIEventArgsBase>();
        }
    }

    private void Update()
    {
        if (eventQueue.Count > 0 && OnForwardedEvent != null )
        {
            UIEventArgsBase args = eventQueue.Dequeue();
            OnForwardedEvent(args);
        }
    }

    /// <summary>
    /// Enqueue args for new events, which get continously sent/raised.
    /// The receiver differentiates, whether or not the event is for them by checking the args' source/target etc.
    /// </summary>
    /// <param name="eventArgs">UIEventArgsBase type</param>
    public void EnqueueEvent( UIEventArgsBase eventArgs )
    {
        // FIXME: LH: this is a bit of a hack, but issue due to timing-
        // this start might not always be called before other Starts(), who want
        // to already enqueue. So we do it twice?
        if (eventQueue == null)
        {
            eventQueue = new Queue<UIEventArgsBase>();
        }

        eventQueue.Enqueue( eventArgs );
    }
}
