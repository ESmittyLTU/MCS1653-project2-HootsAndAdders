using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int nextWaypointIndex;
    public Vector3 endOfLadder;
    public int travelChance = 2;
    //Chance out of 10 that an enemy will travel, 2 being the 20%, and 10 being 100%

    private void Start()
    {
        endOfLadder = transform.parent.GetChild(0).transform.position;
        Debug.Log(endOfLadder.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        if (other.CompareTag("Enemy"))
        {
            if (Random.Range(0, 10) < travelChance)
            {
                Debug.Log("ITS AN ENEMY!");
                EnemyPathing enemy = other.GetComponent<EnemyPathing>();
                enemy.ladderEnd = endOfLadder;
                enemy.onLadder = true;
                enemy.destination = nextWaypointIndex;
            }
        }
    }
}
