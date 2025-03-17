using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Vector3[] Waypoints;
    public float[] SegmentDistances;

    // Start is called before the first frame update
    void Start()
    {
       /*   Might not need this due to using different evaluation of 
        SegmentDistances = new float[Waypoints.Length];
        SegmentDistances[0] = 0;
        for (int i = 1; i < SegmentDistances.Length; i++)
        {
            SegmentDistances[i] = Vector3.Distance(Waypoints[i - 1], Waypoints[i]);
        }
       */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetDistanceRemaining(Vector3 position, int nextWaypoint)
    {
        //Dont use all segments and for loops, just multiply distance by 1/100 since distance will never be greater than 100, then add to index of current waypoint
        //Now have a float with first digits showing next waypoint, and digits after decimal stating how far from current waypoint
        float distanceTotal = (Vector3.Distance(position, Waypoints[nextWaypoint]) * 0.01f) + Waypoints.Length - nextWaypoint;

        return (distanceTotal);
    }
}