using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int nextWaypointIndex;
    public Vector3 endOfLadder;

    private void Start()
    {
        endOfLadder = transform.parent.GetChild(0).transform.position;
        Debug.Log(endOfLadder.ToString());
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyPathing>().destination = nextWaypointIndex;
        }
    }
}
