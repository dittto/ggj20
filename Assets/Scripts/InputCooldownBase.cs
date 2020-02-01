using System.Collections.Generic;
using UnityEngine;


// Note:    Cooldown checking might not always be related to resetting the cooldown 
//          (i.e. we can check if cooldown is still ticking without needing to update it).

//          So it is the derived class' responsibility to update the cooldown upon usage.
public class InputCooldownBase : MonoBehaviour
{
    [SerializeField]
    protected float timer;

    // Note: not serialized on purpose to make child class set them however it wants
    protected float[] cooldowns;
    protected float[] nextInputTime;

    protected void InitialiseCooldownTimer( int cooldownsAmount )
    {
        cooldowns       = new float[cooldownsAmount];
        nextInputTime   = new float[cooldownsAmount];
    }

    protected void SetCooldown( int cooldownId, float cooldown)
    {
        if ( cooldownId < cooldowns.Length )
        {
            cooldowns[cooldownId] = cooldown;
        }
    }

    protected void ResetTimer()
    {
        timer = 0;
    }

    protected void Tick()
    {
        timer += Time.deltaTime;
    }

    protected bool IsInputOnCooldown( int cooldownId )
    {
        if ( cooldownId < nextInputTime.Length )
        { 
            return ( nextInputTime[cooldownId] >= timer );
        }

        return false;
    }

    protected void UpdateInputCooldown( int cooldownId )
    {
        // Sanity check input, if cooldown id exists in array
        if ( cooldownId < cooldowns.Length &&
            cooldownId < nextInputTime.Length )
        {
            if ( !IsInputOnCooldown(cooldownId) )
            {
                nextInputTime[cooldownId] = timer + cooldowns[cooldownId];
            }
        }
    }
}
