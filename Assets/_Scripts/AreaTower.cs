using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTower : MonoBehaviour
{
    public float range = 4f;
    public float shotDelay = 1f;
    public EnemyPathing target;
    public MainPath levelPath;
    public GameObject firingEffect;
    public AudioClip basicDeath;

    private float canShoot = 0;
    private float currentEnemyDistance;
    private Vector3 targetPos;

    void Start()
    {
        levelPath = MainPath._path;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPathing[] possibleTargets = FindObjectsOfType<EnemyPathing>();

        if (possibleTargets.Length == 0) return; //Dont do anything if no enemies

        //Starting with possible target 0, compare each target to the current target, and if it is closer to the tower, make that the new target
        //target = possibleTargets[0];

        //if no target currently selected
        //target = null;

        canShoot += Time.deltaTime;
        if (canShoot >= shotDelay)
        {
            foreach (EnemyPathing enemy in possibleTargets)
            {
                //check if this target is in range
                if (Vector3.Distance(transform.position, enemy.transform.position) <= range)
                {
                    //If its a bundle enemy, subtract from its health, otherwise, it is a regular enemy, then subtract from its health and check if it needs to be destroyed
                    if (enemy.gameObject.GetComponent<BundleEnemy>() != null)
                    {
                        enemy.gameObject.GetComponent<BundleEnemy>().health--;
                        continue;
                    }
                    else
                    {
                        enemy.gameObject.GetComponent<EnemyHealth>().health--;

                        //This only checks for base enemies or subclasses of EnemyHealth, my bundle enemy will handle itself
                        if (enemy.gameObject.GetComponent<EnemyHealth>().health <= 0)
                        {
                            AudioSource.PlayClipAtPoint(basicDeath, enemy.transform.position);
                            Destroy(enemy.gameObject);
                        }
                        continue; // move on to next iteration
                    }
                }
            }
            canShoot = 0;
            Instantiate(firingEffect, new Vector3 (transform.position.x, transform.position.y, -2f), Quaternion.identity);
        }
    }
}
