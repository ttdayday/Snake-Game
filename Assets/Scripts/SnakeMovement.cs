using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject snakeTailPrefab;
    public SpawnChicken spawnChicken;
    private Vector2 moveDirection;
    public float stepDelay = 0.5f;
    private float nextStepTime;

    private List<GameObject> snakeSegments = new List<GameObject>();
    void Start()
    {
        Vector2 headPosition = new Vector2(-6, 0);
        Vector2 bodyPosition = new Vector2(-7, 0);
        Vector2 tailPosition = new Vector2(-8, 0);

        // Instantiate and store the head
        GameObject head = Instantiate(snakeHeadPrefab, headPosition, Quaternion.identity);
        snakeSegments.Add(head);

        // Instantiate and store the initial body segment
        GameObject body = Instantiate(snakeBodyPrefab, bodyPosition, Quaternion.identity);
        snakeSegments.Add(body);

        // Instantiate and store the tail
        GameObject tail = Instantiate(snakeTailPrefab, tailPosition, Quaternion.identity);
        snakeSegments.Add(tail);
    }
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
            Grow();
            spawnChicken.SpawnAtRandomLocation();
        }
    }

    public void Grow()
    {
         Vector2 newSegmentPosition = snakeSegments[snakeSegments.Count - 2].transform.position;

        // Instantiate a new body segment at the position of the current tail
        GameObject newSegment = Instantiate(snakeBodyPrefab, newSegmentPosition, Quaternion.identity);

        // Insert the new body segment before the tail in the list
        snakeSegments.Insert(snakeSegments.Count - 1, newSegment);
    }
}
