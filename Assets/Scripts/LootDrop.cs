using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour 
{
    [SerializeField] GameObject lootObject;
    [SerializeField] int percentageDrop;
    [SerializeField] DifficultyManager difficultyManager;

    int _chanceLimit;


    private void Start()
    {
        if (difficultyManager == null)
            difficultyManager = FindObjectOfType<DifficultyManager>();

        percentageDrop = difficultyManager.percentage;

        _chanceLimit = percentageDrop / 10;
    }

    void OnTriggerEnter2D(Collider2D collider)      // when hit by player projectile
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        // select a random object to be dropped with 10% chance for a value to be picked
        int randomPicked = Random.Range(0, 9);

        if (randomPicked < _chanceLimit)
            Instantiate(lootObject, transform.position, transform.rotation);
    }
}
