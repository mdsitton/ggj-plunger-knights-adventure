using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointState : States
{
    public GameObject[] waypointPositions;
    public override States RunCurrentState()
    {
        return this;
    }
}
