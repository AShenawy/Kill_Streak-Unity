using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Range(0f, 50f), SerializeField]
    float Speed = 10f;

    [SerializeField] AudioClip[] damageSFX;
    [SerializeField] float timeBetweenTakingDamage = 1f;
    [SerializeField] GameObject deathParticleEffect;

    [HideInInspector] public bool playerCanMove = true;
    bool facingRight = true;
    bool canTakeDamage = true;
    bool isFlashing = false;
    bool isRunning;
    Animator _animator;
    AudioSource _audioSource;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Fixed update ensures our input is updating with physics simulations on a fixed framerate, thus Time.deltaTime is not necessary for optimisation
    void FixedUpdate()
    {
        float moveHl = Input.GetAxis("Horizontal");
        float moveVl = Input.GetAxis("Vertical");

        if (playerCanMove)
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveHl * Speed, moveVl * Speed);

        // Check if sprite direction is not same as pointed direction
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (mousePosition.x < 0f && facingRight || mousePosition.x > 0f && !facingRight)
            FlipSprite();

        // check if player is moving or standing still
        if (moveHl != 0f || moveVl != 0f)
            isRunning = true;
        else isRunning = false;

        // set animation based on run or stand state
        _animator.SetBool("Run", isRunning);

        if (!playerCanMove)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
        
    void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void TakeDamage (int amount)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            GameManager.gm.LoseLife(amount);
            GameManager.gm.scoreMultiplier = 0;
            PlaySound(damageSFX[Random.Range(0,damageSFX.Length)]);
            Invoke("AllowTakingDamage" , timeBetweenTakingDamage);
        }

        if(!isFlashing)
            StartCoroutine(FlashSprite());
    }

    void AllowTakingDamage ()
    {
        canTakeDamage = true;
    }

    IEnumerator FlashSprite()
    {
        isFlashing = true;
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponentInChildren<WeaponShooter>().FlashWeapon(timeBetweenTakingDamage);

        for (int i = 0; i < 5; i++)
        {
            if ((i % 2) > 0)
                c.a = 0f;
            else c.a = 1f;
            
            GetComponent<SpriteRenderer>().color = c;

            yield return new WaitForSecondsRealtime(timeBetweenTakingDamage / 5);
        }
        
        isFlashing = false;
    }

    void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void DestroyPlayer ()
    {
        Instantiate(deathParticleEffect, transform.position, transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
