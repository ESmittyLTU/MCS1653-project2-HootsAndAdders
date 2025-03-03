using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
