using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;

    void Update()
    {
        HandleInput();
        if (Time.time >= nextStepTime)
        {
            transform.position = new Vector2(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y);
            nextStepTime = Time.time + stepDelay;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }
    }
}
