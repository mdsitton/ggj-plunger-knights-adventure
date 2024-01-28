using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : States
{
    public AttackState attackState;
    public bool isInAttackRange;

    public override States RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
       
    }
}
