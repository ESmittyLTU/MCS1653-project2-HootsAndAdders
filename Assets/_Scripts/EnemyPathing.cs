using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public Transform[] Waypoints;
    public int destination;
    public float speed = 2f, waypointRange = 0.01f;

    // Waypoints should be numbered from 0 to 10 (or max waypoints), not including ladders/chutes
    // when position == current waypoint, set current waypoint to Waypoint[destination+1]
    // Chutes and ladders numbered numerically(1+0)




    void Start()
    {
        destination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //If it reaches the end, subtract health
        if (destination >= Waypoints.Length) {
            GameManager.health--;
            Debug.Log($"Player health is now {GameManager.health}");
            Destroy(gameObject);
        }

        //Move torwards next waypoint
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[destination].position, speed * Time.deltaTime);
        
        //The way I made the scene is wonky, so I have to use the overloaded versions of LookAt + modify child objects so that
        //when parent is rotated, child is facing right direction
        if (destination == 2 || destination == 3 || destination == 6 || destination == 7)
        {
            transform.LookAt(Waypoints[destination], Vector3.down);
        } 
        else
        {
            transform.LookAt(Waypoints[destination], Vector3.up);
        }
        
        if (Vector3.Distance(transform.position, Waypoints[destination].position) <= waypointRange)
        {
            destination++;
        }
    }
}
