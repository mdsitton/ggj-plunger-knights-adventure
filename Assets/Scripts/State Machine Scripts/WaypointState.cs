using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointState : States
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    float rotSpeed;
    public float speed;
    public float WPradius = 1;

    private void Update()
    {
      //  if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position, transform.position) < )
    }

    public override States RunCurrentState()
    {
        return this;
    }
}
