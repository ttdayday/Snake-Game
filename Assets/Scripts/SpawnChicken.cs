using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnChicken : MonoBehaviour
{
    public GameObject chickenPrefab;
    private int gridWidth = 19;
    private int gridHeight = 19;

    void Start()
    {
        SpawnAtRandomLocation();
    }

    public void SpawnAtRandomLocation()
    {
        int x = Random.Range(0, gridWidth);  // 0 to 18
        int y = Random.Range(0, gridHeight); // 0 to 18

        float worldX = x - gridWidth / 2;  // This will give us -9 to 9
        float worldY = y - gridHeight / 2; // This will give us -9 to 9

        // Instantiate the chicken prefab at the calculated world position
        Vector3 spawnPosition = new Vector3(worldX, worldY, 0); // Assuming you want to spawn at z = 0
        Instantiate(chickenPrefab, spawnPosition, Quaternion.identity);
    }
}
