using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public float waitTime = 1f;

    // Update is called once per frame
    void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            Destroy(gameObject);
        }
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
