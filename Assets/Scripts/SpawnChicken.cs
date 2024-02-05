using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnChicken : MonoBehaviour
{
    public GameObject chickenPrefab;
    private int gridWidth = 19;
    private int gridHeight = 19;
    public SnakeMovement snakeMovement;
  
    void Start()
    {
        SpawnAtRandomLocation();
    }

    public void SpawnAtRandomLocation()
    {
        // Ensure there's a reference to SnakeMovement
        if (!snakeMovement)
        {
            Debug.LogError("SnakeMovement reference not set in SpawnChicken.");
            return;
        }


        bool isOccupied;
        int x, y;
        Vector3 spawnPosition;
        do
        {
            isOccupied = false;
            x = Random.Range(0, gridWidth);  // 0 to 18
            y = Random.Range(0, gridHeight); // 0 to 18

            float worldX = x - gridWidth / 2;  // This will give us -9 to 9
            float worldY = y - gridHeight / 2; // This will give us -9 to 9
            spawnPosition = new Vector3(worldX, worldY, 0); // Assuming you want to spawn at z = 0

            // Check if this position is occupied by the snake
            foreach (var segment in snakeMovement.GetSnakePosition())
            {
                if ((int)segment.x == (int)worldX && (int)segment.y == (int)worldY)
                {
                    isOccupied = true;
                    break; // This location is occupied, break and choose another location
                }
            }
        } while (isOccupied); // Repeat until an unoccupied location is found

        // Instantiate the chicken prefab at the unoccupied location
        Instantiate(chickenPrefab, spawnPosition, Quaternion.identity);
    }
    

}
