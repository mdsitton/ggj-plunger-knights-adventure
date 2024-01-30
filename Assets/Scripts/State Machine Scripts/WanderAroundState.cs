using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAroundState : StateOneShot
{
    public float speed = 5f;
    private float angle = 0f;
    private Vector2 startingPostion;
    private float radius = 2f;
    private Rigidbody2D body;

    public float MinMoveDistance = 1f;

    private int enemyMask;
    private Vector2 targetPoint;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        var enemy = GetComponent<Enemy>();
        speed = enemy.speed;
        radius = enemy.radius;
        enemyMask = LayerMask.GetMask("Enemy", "Player");
        startingPostion = body.position;
        targetPoint = RandomPointInRadius();
    }

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        targetPoint = RandomPointInRadius();
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        if (Vector2.Distance(body.position, targetPoint) <= 0.1f)
        {
            targetPoint = RandomPointInRadius();
        }

        if (Vector2.Distance(body.position, startingPostion) > radius)
        {
            targetPoint = RandomPointInRadius();
        }

        MoveTowardsPoint(targetPoint);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        targetPoint = RandomPointInRadius();
    }

    private float rotationSpeed = 9000f; // rotate  degrees per second

    private void MoveTowardsPoint(Vector2 targetPoint)
    {
        Vector2 direction = targetPoint - body.position;
        body.velocity = direction.normalized * speed;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        body.SetRotation(rotation);
    }

    private Vector2 RandomPointInRadius()
    {
        float distance = Random.Range(0f, radius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 randomPoint = (Vector2)transform.position + randomDirection * distance;

        // Ensure the random point is within the radius of the starting position
        Vector2 directionFromStartingPosition = randomPoint - startingPostion;
        if (directionFromStartingPosition.magnitude > radius)
        {
            randomPoint = startingPostion + directionFromStartingPosition.normalized * radius;
        }

        // Ensure the distance is greater than a configurable distance from the transform position
        if (Vector2.Distance(randomPoint, transform.position) < MinMoveDistance)
        {
            randomPoint = (Vector2)transform.position + (randomPoint - (Vector2)transform.position).normalized * MinMoveDistance;
        }

        return randomPoint;
    }
}
