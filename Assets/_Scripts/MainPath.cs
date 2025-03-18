using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPath : MonoBehaviour
{
    public static MainPath _path;

    public Vector3[] Waypoints;
    //public float[] SegmentDistances;

    // Start is called before the first frame update
    void Start()
    {
        _path = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetDistanceRemaining(Vector3 position, int nextWaypoint)
    {
        //Dont use all segments and for loops, just multiply distance by 1/100 since distance will never be greater than 100, then add to index of current waypoint
        //Now have a float with first digits showing next waypoint, and digits after decimal stating how far from current waypoint
        //Works better for ladder mechanic, so that as soon as an enemy enters the ladder/chute, they're automatically farther ahead/behind and towers target accordingly
        float distanceTotal = (Vector3.Distance(position, Waypoints[nextWaypoint]) * 0.01f) + Waypoints.Length - nextWaypoint;

        return (distanceTotal);
    }
}
