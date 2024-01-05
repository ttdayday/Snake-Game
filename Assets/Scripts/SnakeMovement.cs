using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    // Start is called before the first frame update
   
    private Vector2Int gridPosition;
    private Vector2Int moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;
    private Vector2Int nextDirection;
    private int startX = -6;
    private int startY = 0;


    void Start()
    {
        gridPosition = new Vector2Int(startX,startY);
        moveDirection = Vector2Int.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        if (Time.time >= nextStepTime)
        {            
            if (nextDirection != Vector2Int.zero)
            {
                moveDirection = nextDirection; 
            }

            Move();
            nextStepTime = Time.time + stepDelay;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextDirection = Vector2Int.up;
            Debug.Log("UP");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            nextDirection = Vector2Int.down;
            Debug.Log("down");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            nextDirection = Vector2Int.left;
            Debug.Log("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            nextDirection = Vector2Int.right;
            Debug.Log("right");
        }
    }

    private void Move()
    {
        gridPosition += moveDirection;
        transform.position = new Vector3(gridPosition.x, gridPosition.y, transform.position.z);

        if (moveDirection != Vector2Int.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        Debug.Log("Grid Position:" + gridPosition + ", Move Direction:" + moveDirection);

    }


       

}
