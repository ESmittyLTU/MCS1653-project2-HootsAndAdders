using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 4f;
    public GameObject bullet;
    public float shotDelay = 1f;

    private GameObject target;
    private Transform aimer;
    private float canShoot = 0;

    void Start()
    {
        target = GameObject.Find("Enemy");
        aimer = transform.Find("Head");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Enemy");

        if (possibleTargets.Length == 0) return; //Dont do anything if no enemies

        //Starting with possible target 0, compare each target to the current target, and if it is closer to the tower, make that the new target
        target = possibleTargets[0];
        foreach (GameObject o in possibleTargets)
        {
            if (Vector3.Distance(transform.position, o.transform.position) < 
                Vector3.Distance(transform.position, target.transform.position))
            {
                target = o;
            }
        }

        // Take the target's x and y but the aimer's Z so that when the aimer is looking around it doesnt bend through 3D space
        Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, aimer.transform.position.z);
        aimer.LookAt(targetPos);

        canShoot += Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPos) <= range)
        {
            if (canShoot >= shotDelay)
            {
                Instantiate(bullet, aimer.position, aimer.GetChild(0).rotation);
                canShoot = 0;
            }
        }
    }
}
