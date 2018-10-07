using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlowdown : MonoBehaviour 
{
    AudioSource audioSource;


    private void OnEnable()
    {
        EventManager.KillStreakInitiatedEvent += SlowdownAudio;
        EventManager.KillStreakStopEvent += SpeedupAudio;
    }

    private void OnDisable()
    {
        EventManager.KillStreakInitiatedEvent -= SlowdownAudio;
        EventManager.KillStreakStopEvent -= SpeedupAudio;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SlowdownAudio ()
    {
        audioSource.pitch = 0.75f;
    }

    void SpeedupAudio ()
    {
        audioSource.pitch = 1.1f;
    }
}
