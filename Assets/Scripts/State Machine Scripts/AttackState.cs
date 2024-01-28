using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : States
{
    public override States RunCurrentState()
    {
        Debug.Log("I have attacked");
        return this;
    }
}
