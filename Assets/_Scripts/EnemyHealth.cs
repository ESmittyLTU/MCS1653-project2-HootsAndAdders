using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public AudioClip basicDeath;
    public GameObject featherExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Instantiate(featherExplosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(basicDeath, transform.position);
                GameManager.money++;
                GameManager.moneyCounter.SetText($"{GameManager.money}");
                Destroy(gameObject);
            }
        }
    }
}
