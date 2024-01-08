using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    // Start is called before the first frame update
   
    private Vector2Int headPosition;
    private Vector2Int moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;
    private Vector2Int nextDirection;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
 

   


    void Start()
    {
        headPosition = new Vector2Int(-6,0);
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
                startPosition = transform.position;
                headPosition += moveDirection;
                endPosition = new Vector3(headPosition.x, headPosition.y, transform.position.z);
                t = 0;
                nextStepTime = Time.time + stepDelay;
            }
                        
        }
        if (moveDirection != Vector2Int.zero)
        {
            t += Time.deltaTime / stepDelay;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2Int.down)
        {
            nextDirection = Vector2Int.up;
            Debug.Log("UP");
        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector2Int.up)
        {
            nextDirection = Vector2Int.down;
            Debug.Log("down");
        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector2Int.right)
        {
            nextDirection = Vector2Int.left;
            Debug.Log("left");
        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector2Int.left)
        {
            nextDirection = Vector2Int.right;
            Debug.Log("right");
        }
    }

    public Vector2Int GetCurrentHeadPosition()
    {
        return headPosition;
    }

    public Vector2Int GetMoveDirection()
    {
        return moveDirection;
    }
}




