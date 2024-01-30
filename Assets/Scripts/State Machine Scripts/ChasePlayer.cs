using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateOneShot
{
    public float speed = 5f;
    private Rigidbody2D body;

    private IAttackable currentTarget;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        var enemy = GetComponent<Enemy>();
        speed = enemy.speed;
    }

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        currentTarget = stateData.entity.CurrentTarget;
        Debug.Log("Set player target");
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        if (currentTarget != null)
        {
            MoveTowardsPoint(currentTarget.GameObject.transform.position);
            Debug.Log("Chasing player");
        }
    }

    private float rotationSpeed = 9000f; // rotate  degrees per second

    private void MoveTowardsPoint(Vector2 targetPoint)
    {
        Vector2 direction = targetPoint - body.position;
        body.velocity = direction.normalized * speed;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        body.SetRotation(rotation);
        Debug.DrawLine(body.position, targetPoint, Color.red);
    }
}
