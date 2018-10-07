using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_EnemyBehaviour : MonoBehaviour 
{
    [Header("Parameters")]
    [SerializeField] int damageAmount = 1;
    [SerializeField] int scoreValue = 10;
    [Header("Properties")]
    [SerializeField] GameObject deathParticleFX;


    void OnCollisionEnter2D (Collision2D collider)
    {
        // reduce player life by damageAmount
        if (collider.gameObject.CompareTag("Player"))
            collider.gameObject.GetComponent<CharacterController2D>().TakeDamage(damageAmount);
    }

    public void CommitDeath ()
    {
        Instantiate(deathParticleFX, transform.position, transform.rotation);
        GameManager.gm.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
