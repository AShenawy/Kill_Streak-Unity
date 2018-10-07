using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] bool timedDestruction = false;
    [SerializeField] float waitForDestroy = 1f;

    void Update()
    {
        if (timedDestruction)
            DestroyObject(gameObject, waitForDestroy);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
