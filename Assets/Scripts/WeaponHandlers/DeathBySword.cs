using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBySword : MonoBehaviour 
{


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the FollowTarget script to stop chasing player
            Destroy(GetComponent<FollowTarget>());

            // Disable collisions with other gameObjects
            GetComponent<CircleCollider2D>().enabled = false;

            // Destroy rigid body component
            Destroy(GetComponent<Rigidbody2D>());
            
            // Set the sword/bullet as parent
            transform.SetParent(collision.gameObject.transform);
            GetComponent<SpriteRenderer>().sortingOrder = 5;

            transform.localPosition = Vector2.up;
            //Die();
        }
    }

    /*void Die()
    {
        print("I died!");
        GetComponent<CircleCollider2D>().enabled = false;
                
    }
    */

   
}
