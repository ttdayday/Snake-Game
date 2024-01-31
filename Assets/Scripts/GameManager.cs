using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SnakeMovement snakeMovement;
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject snakeTailPrefab;

    private SpawnChicken chikenSpawner;
    void Start()
    {
        chikenSpawner = GetComponent<SpawnChicken>();

        Vector2 headPosition = new Vector2(-6, 0);
        Vector2 bodyPosition = new Vector2(-7, 0);
        Vector2 bodyBPosition = new Vector2(-8, 0);
        Vector2 tailPosition = new Vector2(-9, 0);

        // Instantiate and store the head
        GameObject head = Instantiate(snakeHeadPrefab, headPosition, Quaternion.identity);
        SnakeMovement movement = head.GetComponent<SnakeMovement>();
        movement.Initialize(chikenSpawner);

        // Add the head to the snakeSegments list in SnakeMovement
        movement.AddSnakeSegment(head);

        // Instantiate and store the initial body segment
        GameObject body = Instantiate(snakeBodyPrefab, bodyPosition, Quaternion.identity);
        // Add the body to the snakeSegments list in SnakeMovement
        movement.AddSnakeSegment(body);

        // Instantiate and store the initial body segment
        GameObject bodyB = Instantiate(snakeBodyPrefab, bodyBPosition, Quaternion.identity);
        // Add the body to the snakeSegments list in SnakeMovement
        movement.AddSnakeSegment(bodyB);

        // Instantiate and store the tail
        GameObject tail = Instantiate(snakeTailPrefab, tailPosition, Quaternion.identity);
        // Add the tail to the snakeSegments list in SnakeMovement
        movement.AddSnakeSegment(tail);
    }

    // Update is called once per frame


    void Update()
    {
        
    }
}