using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerBehaviour : MonoBehaviour 
{
    [Header("Parameters")]
    [SerializeField] int damageAmount = 1;
    [SerializeField] int scoreValue = 10;
    // [SerializeField] float detectRadius = 7f;  ============= will follow target automatically
    // [SerializeField] LayerMask detectWhat;   ============= will follow target automatically
    [Header("Properties")]
    [SerializeField] GameObject[] bloodDecals;
    [SerializeField, Range(0f, 1.5f)] float minBloodDecalSize = 0.5f;
    [SerializeField, Range(0.5f, 2f)] float maxBloodDecalSize = 1.2f;
    [SerializeField] GameObject bleedParticleFX;
    [SerializeField] AudioClip onSpawnSFX;
    [SerializeField] AudioClip DeathSFX;

    bool _isDead = false;
    Animator _animator;
    AudioSource _audioSource;

    private void Start()
    {
        // assign variables
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        PlaySound(onSpawnSFX);
    }

    // ============== Below Commented Out So Enemy Now Follows Target Automatically ==============
    //private void FixedUpdate()
    //{
    //    Collider2D contact = Physics2D.OverlapCircle(transform.position, detectRadius, detectWhat); // look for the player character to chase
    //    if (contact != null)
    //        if (contact.CompareTag("Player"))
    //            GetComponent<FollowTarget>().isActive = true;
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // reduce player life by damageAmount
            collision.gameObject.GetComponent<CharacterController2D>().TakeDamage(damageAmount);
    }

    void OnTriggerEnter2D(Collider2D collider)      // get killed by bullets
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Die(collider.transform);
            StartCoroutine(Bleed());
        }
    }

    private void OnDestroy()
    {
        if (_isDead)
        {
            for (int i = 0; i < 3; i++)
            {
                int randomPicked = Random.Range(0, bloodDecals.Length);
                float randomX = Random.Range(-0.5f, 0.5f);
                float randomY = Random.Range(-0.5f, 0.5f);
                float rndX = (float)System.Math.Round(randomX, 2);
                float rndY = (float)System.Math.Round(randomY, 2);

                GameObject corpseBlood = Instantiate(bloodDecals[randomPicked], transform.position, transform.rotation);
                corpseBlood.transform.position = new Vector2(transform.position.x + rndX, transform.position.y + rndY);
            }
            _isDead = false;
        }
    }

    void Die(Transform projectile)
    {
        GetComponent<FollowTarget>().isActive = false; // stop chasing player
        GetComponent<Rigidbody2D>().simulated = false; // disable collisions and physics
        GameManager.gm.AddScore(scoreValue);
        transform.SetParent(projectile);
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        _animator.SetTrigger("Dead");
        PlaySound(DeathSFX);
        Instantiate(bleedParticleFX, transform);
        _isDead = true;
    }

    IEnumerator Bleed ()
    {
        while (gameObject.activeInHierarchy)
        {
            // Select random blood sprite
            int randomPicked = Random.Range(0, bloodDecals.Length);

            // set random floats for scale then round them to 2 decimal places
            float randomFloat = Random.Range(minBloodDecalSize, maxBloodDecalSize);
            float roundedScale = (float)System.Math.Round(randomFloat, 2);

            // Create random rotation about Z axis
            Quaternion randomRotated = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

            // instantiate the blood decal with random rotation
            GameObject bloodDecal = Instantiate(bloodDecals[randomPicked], transform.position, randomRotated);

            // give the blood decal a random scale
            bloodDecal.transform.localScale = new Vector2(roundedScale, roundedScale);

            yield return new WaitForSeconds(0.05f);
        }
    }

    void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
