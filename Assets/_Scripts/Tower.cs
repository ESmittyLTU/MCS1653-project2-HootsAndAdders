using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    public float range = 4f;
    public GameObject bullet;
    public float shotDelay = 1f;
    public EnemyPathing target;

    private Path path;
    private Transform aimer;
    private float canShoot = 0;
    private float currentEnemyDistance;
    private Vector3 targetPos;

    void Start()
    {
        path = GameObject.Find("Path").GetComponent<Path>();
        aimer = transform.Find("Head");
        aimer.LookAt(new Vector3(-9.5f, -4f, aimer.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPathing[] possibleTargets = FindObjectsOfType<EnemyPathing>();

        if (possibleTargets.Length == 0) return; //Dont do anything if no enemies

        //Starting with possible target 0, compare each target to the current target, and if it is closer to the tower, make that the new target
        target = possibleTargets[0];

        //if no target currently selected
        target = null;

        foreach (EnemyPathing enemy in possibleTargets)
        {
            //check if this target is in range
            if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
            {
                if (target == null)
                {
                    target = enemy;
                    currentEnemyDistance = path.GetDistanceRemaining(target.transform.position, target.destination);
                    continue; // move on to next iteration
                }

                if (path.GetDistanceRemaining(enemy.transform.position, enemy.destination)
                    < currentEnemyDistance)
                {
                    target = enemy;
                    currentEnemyDistance = path.GetDistanceRemaining(target.transform.position, target.destination);
                }
            }
        }

        // Take the target's x and y but the aimer's Z so that when the aimer is looking around it doesnt bend through 3D space
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, aimer.transform.position.z);
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
