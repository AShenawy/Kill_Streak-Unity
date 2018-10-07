using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class WeaponShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 1200f;
    [SerializeField] float reloadTime = 1f;
    [SerializeField] Sprite readyShotSprite;
    [SerializeField] Sprite reloadSprite;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip reloadSFX;

    //[HideInInspector]
    public bool canShoot = true;
    
    void Update () 
	{
        if (Input.GetButton("Fire1") && canShoot && Time.timeScale > 0f)
            if (bulletPrefab)
                ShootBullet();
    }

    void ShootBullet ()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.up, transform.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
        GetComponent<SpriteRenderer>().sprite = reloadSprite;
        canShoot = false;
        Invoke ("Reload", reloadTime);
        PlaySound(shootSFX);
    }

    public void Reload ()
    {
        canShoot = true;
        GetComponent<SpriteRenderer>().sprite = readyShotSprite;
        CancelInvoke("Reload");
        PlaySound(reloadSFX);
    }

    void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void FlashWeapon (float damageTime)
    {
        StartCoroutine(FlashSprite(damageTime));
    }

    IEnumerator FlashSprite (float waitTime)
    {
        Color c = GetComponent<SpriteRenderer>().color;

        for (int i = 0; i < 5; i++)
        {
            if ((i % 2) > 0)
                c.a = 0f;
            else c.a = 1f;

            GetComponent<SpriteRenderer>().color = c;

            yield return new WaitForSecondsRealtime(waitTime / 5);
        }
    }
}
