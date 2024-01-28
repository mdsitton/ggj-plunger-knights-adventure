using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderAroundState : States
{
    public float speed = 5f;
    private float angle = 0f;
    private Vector2 startingPostion;
    private float radius = 2f;
    private Rigidbody2D body;


    public override States RunCurrentState()
    {
        if (true)
        {
            transform.position = new Vector3(0,0,0);
        }
        return this;
    }

    private void Update()
    {
        // Move in a circle for testing
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        Vector2 newPosition = startingPostion + new Vector2(x, y);
       // body.MovePosition(newPosition);
    }
}
