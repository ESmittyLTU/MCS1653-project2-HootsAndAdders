using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int health = 10;
    public GameObject enemy;
    public float spawnDelay = 2f;

    private float canSpawn;

    private void Start()
    {
        Debug.Log($"Player health starting at {health}");
    }

    private void Update()
    {
        canSpawn += Time.deltaTime;
        if (canSpawn >= spawnDelay)
        {
            Instantiate(enemy, new Vector3(-10.5f, -3.9f, 0), Quaternion.identity);
            canSpawn = 0;
        }
    }
}
