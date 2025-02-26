using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public Vector3[] Waypoints;
    public int destination;

    // Waypoints should be numbered from 0 to 10 (or max waypoints), not including ladders/chutes
    // when position == current waypoint, set current waypoint to Waypoint[destination+1]
    // Chutes and ladders numbered numerically(1+0)




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
