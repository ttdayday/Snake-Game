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

    private List<GameObject> snakeSegments = new List<GameObject>();
    void Start()
    {
        
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
}
