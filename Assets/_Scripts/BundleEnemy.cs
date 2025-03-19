using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class BundleEnemy : MonoBehaviour
{
    public GameObject basicEnemy;
    public int health = 3;
    public int spawnCount = 3;
    public float spawnInterval = 0.25f;

    private Vector3 spawnPoint, ladderDest;
    private int destination;
    private bool onLadder = false;
    private EnemyPathing pathingScript;
    private bool alreadySpawned;

    void Start()
    {
        pathingScript = gameObject.GetComponent<EnemyPathing>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
        }
    }

    void spawnEnemiesOnDeath()
    {
        spawnPoint = transform.position;

        onLadder = pathingScript.onLadder;
        destination = pathingScript.destination;
        ladderDest = pathingScript.ladderEnd;
        
        Destroy(pathingScript);

        if (!alreadySpawned)
        {
            alreadySpawned = true;
            for (int i = 0; i < spawnCount; i++)
            {
                Invoke("spawnSingleEnemy", spawnInterval * i);
                if (i == spawnCount - 1)
                {
                    Invoke("thisDies", spawnInterval * i);
                }
            }
        }
    } 

    void thisDies()
    {
        Destroy(gameObject);
    }

    void spawnSingleEnemy()
    {
        GameObject spawnedEnemy = Instantiate(basicEnemy, spawnPoint, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyPathing>().setValuesOnSpawn(onLadder, ladderDest, destination);
    }

    void Update()
    {
        if (health <= 0)
        {   
            spawnEnemiesOnDeath();
        }
    }
}
