using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public SpawnChicken SpawnChicken;
    private Vector2 moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;
    public GameObject snakeBodyPrefab;

    private List<GameObject> snakeSegments = new List<GameObject>();
    private List<Vector2> snakePosition = new List<Vector2>();

    private void Start()
    {
        //foreach (var segment in snakeSegments)
        //{
        //    segmentPositions.Add(segment.transform.position);
        //}
    }
    void Update()
    {
        HandleInput(); 
        if (Time.time >= nextStepTime)
        {
            MoveSnake(); 
            nextStepTime = Time.time + stepDelay;
        }
    }

    public List<Vector2> GetSnakePosition()
    {
        return snakePosition;
    }
    private void MoveSnake()
    {
        if(moveDirection.magnitude == 0)
        {
            return;
        }

        // Move each segment to the position of the segment in front of it
        for (int i = 0; i < snakeSegments.Count; i++)
        {
            snakePosition[i] = snakeSegments[i].transform.position;
        }

        transform.position = new Vector2(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y);


        for (int i = 1; i < snakeSegments.Count; i++)
        {
            snakeSegments[i].transform.position = snakePosition[i - 1];
        }

        // Move the head

    }

    public void AddSnakeSegment(GameObject segment)
    {
        snakeSegments.Add(segment);
        snakePosition.Add(Vector2.zero);
    }

    private void HandleInput()
    {
        // Check for input and also ensure the snake can't reverse onto itself
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2.down)
        {
            moveDirection = Vector2.up;
            RotateSnakeHead(moveDirection);
        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector2.up)
        {
            moveDirection = Vector2.down;
            RotateSnakeHead(moveDirection);
        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector2.right)
        {
            moveDirection = Vector2.left;
            RotateSnakeHead(moveDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector2.left)
        {
            moveDirection = Vector2.right;
            RotateSnakeHead(moveDirection);
        }
    }

    private void RotateSnakeHead(Vector2 dir)
    {
        // Assuming the snake's sprite is facing up by default.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Grow()
    {
        // Check if there are enough segments for growth logic.
        if (snakeSegments.Count < 2)
        {
            Debug.LogError("Not enough segments to apply growth logic.");
            return;
        }

        // The position for the new segment is the current position of the last body segment before the tail.
        // This is the second to last item in the snakeSegments list.
        Vector2 positionForNewSegment = snakeSegments[snakeSegments.Count - 2].transform.position;

        // Instantiate the new body segment at the position of the last body segment.
        GameObject newSegment = Instantiate(snakeBodyPrefab, positionForNewSegment, Quaternion.identity);

        // Insert the new body segment into the list right before the tail segment.
        // This ensures the tail remains the last item in the list.
        snakeSegments.Insert(snakeSegments.Count - 1, newSegment);
        // Also, insert the position in the snakePosition list for consistent logic.
        snakePosition.Insert(snakePosition.Count - 1, positionForNewSegment);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chicken"))
        {
            Destroy(other.gameObject);
            Grow();
            SpawnChicken.SpawnAtRandomLocation();
        }
    }


    public void SetSnakeHead(GameObject head)
    {
        snakeSegments.Insert(0, head);
        // Set this snakeMovement script to the head object so it can control it
        head.GetComponent<SnakeMovement>().enabled = true;
        
    }

    internal void Initialize(SpawnChicken chikenSpawner)
    {
        SpawnChicken = chikenSpawner;
    }
    
}
