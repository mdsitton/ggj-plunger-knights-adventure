using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderAroundState : StateOneShot
{
    public float speed = 5f;
    private float angle = 0f;
    private Vector2 startingPostion;
    private float radius = 2f;
    private Rigidbody2D body;

    public override void OnStateTrigger(CurrentStateData stateData)
    {
        body = stateData.entity.GameObject.GetComponent<Rigidbody2D>();
        startingPostion = body.position;
    }

    public override void OnStateUpdate(CurrentStateData stateData)
    {
        // Move in a circle for testing
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        Vector2 newPosition = startingPostion + new Vector2(x, y);
        body.MovePosition(newPosition);
    }
}
