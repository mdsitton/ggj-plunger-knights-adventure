using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class IdleState : States
{
    public ChaseState chaseState;
    public bool canSeeThePlayer;
    public override States RunCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }

    
}
