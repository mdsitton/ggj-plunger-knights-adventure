using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    float rotSpeed;
    public float speed = 2;
    public float WPradius = 1;

   public void CurrentWaypoint()
    {
       // waypoints[currentWaypoint].transform.position 
    }

    private void Update()
    {
       /* int index = 0;
        Vector3 destination = waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[index].transform.position, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if(distance <= 0.05)*/
    }
}
