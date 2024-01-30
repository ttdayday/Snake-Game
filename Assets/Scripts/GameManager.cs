using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SnakeMovement snakeMovement;
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject snakeTailPrefab;
    private List<GameObject> snakeSegments = new List<GameObject>();
    // Start is called before the first frame update
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

        if (snakeMovement != null)
        {
            snakeMovement.SetSnakeHead(head);
        }
        else
        {
            Debug.LogError("SnakeMovement script not assigned in GameManager.");
        }
    }

    // Update is called once per frame


    void Update()
    {
        
    }
}
