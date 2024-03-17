using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public GameManager gameManager;

    public SpawnChicken SpawnChicken;
    private Vector2 moveDirection;
    private Vector2 bufferedDirection;

    public float stepDelay = 0.3f;
    private float nextStepTime;
    public GameObject snakeBodyPrefab;

    private List<GameObject> snakeSegments = new List<GameObject>();
    private List<Vector2> snakePosition = new List<Vector2>();

    private int gridWidth = 19;
    private int gridHeight = 19;
    public GameObject headDeadPrefab;

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
            // Update moveDirection with the buffered direction right before moving
            if (moveDirection != bufferedDirection)
            {
                moveDirection = bufferedDirection;
                RotateSnakeHead(moveDirection); // Rotate the head here, ensuring it's in sync with the direction change
            }

            Vector2 nextPosition = (Vector2)transform.position + moveDirection;
            if (!WillHitBoundary(nextPosition))
            {
                MoveSnake();
                nextStepTime = Time.time + stepDelay;
            }
            else
            {
                GameOver();
            }
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
        if (Input.GetKeyDown(KeyCode.W) && moveDirection != Vector2.down)
        {
            bufferedDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDirection != Vector2.up)
        {
            bufferedDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDirection != Vector2.right)
        {
            bufferedDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDirection != Vector2.left)
        {
            bufferedDirection = Vector2.right;
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
        Debug.Log("Collided with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Chicken"))
        {
            Destroy(other.gameObject);
            Grow();
            SpawnChicken.SpawnAtRandomLocation();
        }
        else if (other.gameObject.CompareTag("SnakeBody"))
        {
            if (!IsFirstTwoBodySegments(other.gameObject))
            {
                GameOver(); // Assuming GameOver is a method that handles the game over logic
            }
        }
    }

    private bool IsFirstTwoBodySegments(GameObject segment)
    {
        return segment == snakeSegments[1] || segment == snakeSegments[1];
    }
  


    public void SetSnakeHead(GameObject head)
    {
        snakeSegments.Insert(0, head);
        // Set this snakeMovement script to the head object so it can control it
        head.GetComponent<SnakeMovement>().enabled = true;
        
    }

    public void Initialize(SpawnChicken chikenSpawner, GameManager gameManager)
    {
        SpawnChicken = chikenSpawner;
        this.gameManager = gameManager;
    }

   
    private bool WillHitBoundary(Vector2 nextPosition)
    {
        float leftBoundary = -gridWidth / 2 -1;
        float rightBoundary = gridWidth / 2 +1;
        float topBoundary = gridHeight / 2  +1;
        float bottomBoundary = -gridHeight / 2 -1;

        return nextPosition.x <= leftBoundary || nextPosition.x >= rightBoundary || nextPosition.y <= bottomBoundary || nextPosition.y >= topBoundary;
    }
    private void GameOver()
    {
        Debug.Log("Game Over!");
        enabled = false;
        Color deadColor = headDeadPrefab.GetComponent<SpriteRenderer>().color;
        foreach (GameObject segment in snakeSegments)
        {
            SpriteRenderer segmentRenderer = segment.GetComponent<SpriteRenderer>();
            if (segmentRenderer != null)
            {
                segmentRenderer.color = deadColor;
            }
        }
        Instantiate(headDeadPrefab, transform.position, transform.rotation);
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        else
        {
            Debug.LogError("GameManager not set in SnakeMovement."); 
        }

    }
}
