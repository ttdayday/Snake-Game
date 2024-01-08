using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChicken : MonoBehaviour
{
    public GameObject chickenPrefab;
    private int gridWidth = 19;
    private int gridHeight = 19;
    private Vector2Int initialSpawnPosition = new Vector2Int(3, 0);

    void Start()
    {
        Instantiate(chickenPrefab, new Vector3(initialSpawnPosition.x, initialSpawnPosition.y, 0), Quaternion.identity);
    }

    public void SpawnAtRandomLocation(Vector2Int snakeHeadPosition, Vector2Int snakeMoveDirection)
    {
        Vector2Int spawnPosition;
        do
        {
            spawnPosition = new Vector2Int(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
        }
        while (IsOnSameLine(spawnPosition, snakeHeadPosition, snakeMoveDirection));

        Instantiate(chickenPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
    }

    private bool IsOnSameLine(Vector2Int position, Vector2Int headPosition, Vector2Int moveDirection)
    {
        if (moveDirection.x != 0) // Snake is moving horizontally
        {
            return position.y == headPosition.y;
        }
        else if (moveDirection.y != 0) // Snake is moving vertically
        {
            return position.x == headPosition.x;
        }
        return false;
    }
}