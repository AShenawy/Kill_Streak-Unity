using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviourImpaler : MonoBehaviour
{
    [SerializeField] bool timedDestruction = false;
    [SerializeField] float waitForDestroy = 1f;

    [SerializeField] GameObject audioSource;
    [SerializeField] AudioClip destroyedSFX;

    [HideInInspector] public bool _isKillStreak = false;


    private void Start()
    {
        // look for external audio source as local one will be disabled on bullet destruction
        audioSource = GameObject.Find("AudioOutput");
    }

    void Update()
    {
        if (timedDestruction)
        {
            Invoke("DestroyBullet", waitForDestroy);
            timedDestruction = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (_isKillStreak)
                EventManager.StopKillStreak();
                //GameManager.gm.StopKillStreak();

            PlaySound(destroyedSFX);
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        if (_isKillStreak)
            EventManager.StopKillStreak();
            //GameManager.gm.StopKillStreak();

        PlaySound(destroyedSFX);
        Destroy(gameObject);
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
