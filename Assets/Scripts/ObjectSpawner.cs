using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Details"), SerializeField] GameObject[] spawnObjects;

    public DifficultyManager difficultyManager;
    public float secondsBetweenSpawn = 4f;

    [SerializeField] GameObject spawnIndicator;
    [Header("Spawn Area"), SerializeField] Transform minHzEdge;
    [SerializeField] Transform maxHzEdge;
    [SerializeField] Transform minVlEdge;
    [SerializeField] Transform maxVlEdge;

    [Header("Spawn SFX"), SerializeField] AudioClip SpawnSFX;

    float _timeToSpawn;
    Vector2 _spawnLocation;


    // Use this for initialization
    void Start()
    {
        // initialise spawn timer
        secondsBetweenSpawn = difficultyManager.countdown;

        // Set up first enemy spawn in gameplay
        _timeToSpawn = Time.time + secondsBetweenSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        // don't spawn if player lost
        if (GameManager.gm.gameIsOver)
            return;

        if (Time.time >= _timeToSpawn)
        {
            PrepareSpawning();
            _timeToSpawn = Time.time + secondsBetweenSpawn; // set the next spawn time
        }
    }

    void PrepareSpawning()
    {
        // setup random spawn location
        _spawnLocation.x = Random.Range(minHzEdge.position.x, maxHzEdge.position.x);
        _spawnLocation.y = Random.Range(minVlEdge.position.y, maxVlEdge.position.y);

        Instantiate(spawnIndicator, _spawnLocation, transform.rotation, transform);
        PlaySound(SpawnSFX);

        Invoke("SpawnObject", 0.5f); // Do the actual spawning
    }

    void SpawnObject()
    {
        // select random GameObject from spawnObjects and instantiate it
        int randomPicked = Random.Range(0, spawnObjects.Length);
        Instantiate(spawnObjects[randomPicked], _spawnLocation, transform.rotation, transform);
    }

    void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
