using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public MainPath levelPath;
    public int destination;
    public float speed = 2f, waypointRange = 0.01f;

    // Waypoints should be numbered from 0 to 10 (or max waypoints), not including ladders/chutes
    // when position == current waypoint, set current waypoint to Waypoint[destination+1]
    // Chutes and ladders numbered numerically(1+0)

    void Start()
    {
        destination = 0;
        GameObject obj = GameObject.Find("Path");
        Debug.Log($"Path object name: {obj.transform.name}");
        levelPath = MainPath._path;
        //levelPath = obj.GetComponent<MainPath>();
        //if (levelPath != null)
        //    Debug.Log($"Path component transform name: {levelPath.transform.name}");
        //else
        //    Debug.Log("Path is null");
        //Debug.Log($"Waypoint count: {levelPath.Waypoints.Length}");
    }

    //Protected tells it that this script and child scripts can use this code, Virtual allows child to override it as well
    protected virtual void Update()
    {
        //If it reaches the end, subtract health
        if (destination >= levelPath.Waypoints.Length)
        {
            GameManager.health--;
            Debug.Log($"Player health is now {GameManager.health}");
            Destroy(gameObject);
        }
        
            //Move torwards next waypoint
            transform.position = Vector3.MoveTowards(transform.position, levelPath.Waypoints[destination], speed * Time.deltaTime);

            //The way I made the scene is wonky, so I have to use the overloaded versions of LookAt + modify child objects so that
            //when parent is rotated, child is facing right direction
            if (destination == 2 || destination == 3 || destination == 6 || destination == 7)
            {
                transform.LookAt(levelPath.Waypoints[destination], Vector3.down);
            }
            else
            {
                transform.LookAt(levelPath.Waypoints[destination], Vector3.up);
            }

            //If close to waypoint, select next waypoint
            if (Vector3.Distance(transform.position, levelPath.Waypoints[destination]) <= waypointRange)
            {
                destination++;
            }
        
    }
}
