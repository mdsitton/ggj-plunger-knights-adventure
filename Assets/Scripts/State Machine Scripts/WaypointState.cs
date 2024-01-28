using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointState : StateOneShot
{
  public GameObject[] waypoints;
  int currentWaypoint = 0;
  float rotSpeed;
  public float speed;
  public float WPradius = 1;

  private Rigidbody body;

  private void Awake()
  {
    body = GetComponent<Rigidbody>();
  }

  public override void OnStateTrigger(CurrentStateData stateData)
  {
  }

  public override void OnStateUpdate(CurrentStateData stateData)
  {
    if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) < WPradius)
    {
      currentWaypoint++;
      if (currentWaypoint >= waypoints.Length)
      {
        currentWaypoint = 0;
      }
    }

    Vector3 direction = waypoints[currentWaypoint].transform.position - transform.position;
    Quaternion rotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);

    body.velocity = transform.forward * speed;
  }
}
