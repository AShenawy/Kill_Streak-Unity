using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("Spawner Countdowns"), SerializeField] float[] countdownValues;
    [SerializeField] GameObject[] Spawners;
    [Header("Enemy Speeds"), SerializeField] float[] speedValues;
    [Header("Loot Drop Chance"), SerializeField] int[] percentageValues;

    [HideInInspector] public float countdown;
    [HideInInspector] public float speed;
    [HideInInspector] public int percentage;


    int countdownCounter = 0;
    int speedCounter = 0;
    int percentageCounter = 0;

    private void OnEnable()
    {
        EventManager.ThirtySecondEvent += UpdateCountdown;
        
        EventManager.SixtySecondEvent += UpdateCountdown;
        EventManager.SixtySecondEvent += UpdatePercentage;

        EventManager.TwoMinuteEvent += UpdatePercentage;
        // EventManager.TwoMinuteEvent += UpdateSpeed;
    }

    private void OnDisable()
    {
        EventManager.ThirtySecondEvent -= UpdateCountdown;

        EventManager.SixtySecondEvent -= UpdateCountdown;
        EventManager.SixtySecondEvent -= UpdatePercentage;

        EventManager.TwoMinuteEvent -= UpdatePercentage;
        // EventManager.TwoMinuteEvent -= UpdateSpeed;
    }

    private void Awake()
    {
        // set defaults
        countdown = countdownValues[0];
        speed = speedValues[0];
        percentage = percentageValues[0];
    }

    void UpdateCountdown ()
    {
        if (countdownCounter < countdownValues.Length - 1)
            countdownCounter++;

        countdown = countdownValues[countdownCounter];

        foreach (GameObject spawner in Spawners)
        {
            spawner.GetComponent<ObjectSpawner>().secondsBetweenSpawn = countdown;
        }
    }

    void UpdateSpeed ()
    {
        if (speedCounter < speedValues.Length - 1)
            speedCounter++;

        speed = speedValues[speedCounter];
    }

    void UpdatePercentage ()
    {
        if (percentageCounter < percentageValues.Length - 1)
            percentageCounter++;

        percentage = percentageValues[percentageCounter];
    }
}
