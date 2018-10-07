using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script checks if the projectile hits more than a specified number of enemies and starts a score multiplier
public class KillStreakCheck : MonoBehaviour
{
    [SerializeField] int killStreakThreshold;
    [SerializeField] LayerMask colliderFilter;
    [SerializeField] float boxCheckSizeX;
    [SerializeField] float boxCheckSizeY;
    [SerializeField] AudioClip SloMoSFX;

    float _timerOut = 0f;
    bool _timeStop = false;

    private void Start()
    {
        //print("looking for target");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,new Vector2(boxCheckSizeX,boxCheckSizeY),transform.rotation.z,transform.up, Mathf.Infinity, colliderFilter);

        if (hits.Length > (killStreakThreshold - 1))
        {
            // set the countdown timer before setting the KillStreak events in motion 
            _timerOut = Time.unscaledTime + 1f;
            Time.timeScale = 0.025f;
            GetComponent<AudioSource>().PlayOneShot(SloMoSFX);

            _timeStop = true;
            GetComponent<BulletBehaviourImpaler>()._isKillStreak = true;
        }
    }

    private void Update()
    {

        if (_timeStop)
        {
            if (Time.unscaledTime > _timerOut)
            {
                // Trigger the KillStreak event
                EventManager.InitiateKillStreak();
                _timeStop = false;
            }
        }
    }
}
