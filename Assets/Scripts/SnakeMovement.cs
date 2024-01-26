using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
   
    public SpawnChicken spawnChicken;
    private Vector2 moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;
    public GameObject snakeBodyPrefab;

    public List<GameObject> snakeSegments = new List<GameObject>();
    private List<Vector2> segmentPositions = new List<Vector2>();

    private void Start()
    {
        foreach (var segment in snakeSegments)
        {
            segmentPositions.Add(segment.transform.position);
        }
    }
    void Update()
    {
        HandleInput();
        if (Time.time >= nextStepTime)
        {
            MoveSnake(); 
            transform.position = new Vector2(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y);
            nextStepTime = Time.time + stepDelay;
        }
    }
    private void MoveSnake()
    {
        // Store the current head position
        Vector2 previousPosition = transform.position;

        // Move the head
        transform.position = new Vector2(transform.position.x + moveDirection.x, transform.position.y + moveDirection.y);

        // Move each segment to the position of the segment in front of it
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Vector2 temp = snakeSegments[i].transform.position;
            snakeSegments[i].transform.position = previousPosition;
            previousPosition = temp;
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chicken"))
        {
            Destroy(other.gameObject);
            //Grow();
            spawnChicken.SpawnAtRandomLocation();
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
        spawnChicken = chikenSpawner;
    }
}
