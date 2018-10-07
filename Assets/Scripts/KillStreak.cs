using UnityEngine;
using System.Collections;
using Enemies;
using UnityEngine.UI;

public class KillStreak : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject playerWeapon;
    [SerializeField] GameObject focusBorder;
    [SerializeField, Range(0.1f, 1f)] float slomoTimestep = 0.25f;
    [SerializeField] float bonusModeTime;
    [SerializeField] AudioClip NormalizeTimeSFX;
    [SerializeField] AudioSource audioSource;

    bool _isKillStreak = false;
    bool _isBonus = false;
    float _bonusCountdown;

    private void OnEnable()
    {
        // Subscribe to the events that launch the methods within this script
        EventManager.KillStreakInitiatedEvent += StartKillStreak;
        EventManager.KillStreakStopEvent += EndKillStreak;
    }

    private void OnDisable()
    {
        EventManager.KillStreakInitiatedEvent -= StartKillStreak;
        EventManager.KillStreakStopEvent -= EndKillStreak;
    }

    private void Awake()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        focusBorder.SetActive(false);
    }

    private void Update()
    {
        if (Time.unscaledTime > _bonusCountdown && _isBonus)
        {
            _isBonus = false;
            StartCoroutine(NormalizeTime());
        }

    }

    void LateUpdate()
    {
        if (_isKillStreak)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<FollowTarget>().enabled = false;  // Disable all Stalker enemies
            }
        }
    }

    public void StartKillStreak()
    {
        //Debug.Log("Kill Streak started");
        _isKillStreak = true;
        _isBonus = false;
        DisableActiveGameObjects();
        focusBorder.SetActive(true);
        Cursor.visible = false;

        // make Main Camera follow the bullet in action and zoom in on it
        mainCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = GameObject.FindWithTag("Bullet").transform;
        mainCamera.GetComponent<Camera>().orthographicSize = 3.8f;

        Time.timeScale = slomoTimestep;
    }

    void DisableActiveGameObjects()
    {
        GameObject.FindWithTag("Spawner").GetComponent<ObjectSpawner>().enabled = false;    // Disable Spawner
        GameObject.FindWithTag("Player").GetComponent<CharacterController2D>().playerCanMove = false;   // Disable Player movement
        GameObject.FindWithTag("Player").GetComponentInChildren<WeaponShooter>().enabled = false;  // Disable shooter
    }

    public void EndKillStreak()
    {
        //Debug.Log("Kill Streak ended");
        _isKillStreak = false;
        focusBorder.SetActive(false);
        Cursor.visible = true;

        // return Main Camera to follow Player =============== Solved in Camera2DFollow script
        //mainCamera.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = GameObject.FindWithTag("Player").transform;

        // return Main Camera zoom to normal
        mainCamera.GetComponent<Camera>().orthographicSize = 6.5f;

        EnableActiveGameObjects();

        // give Player quick reload bonus for successful kill streak
        playerWeapon.GetComponent<WeaponShooter>().Reload();
        BonusMode();
    }

    void EnableActiveGameObjects()
    {
        GameObject.FindWithTag("Spawner").GetComponent<ObjectSpawner>().enabled = true;    // Enable Spawner
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<FollowTarget>().enabled = true;  // Enable all Stalker enemies
        }
        GameObject.FindWithTag("Player").GetComponent<CharacterController2D>().playerCanMove = true;   // Enable Player movement
        GameObject.FindWithTag("Player").GetComponentInChildren<WeaponShooter>().enabled = true;  // Enable shooter
    }

    // Time stays in slomo for period slomoTimestep to aim and shoot again
    void BonusMode()
    {
        Time.timeScale = slomoTimestep;
        _bonusCountdown = Time.unscaledTime + bonusModeTime;
        _isBonus = true;
    }

    IEnumerator NormalizeTime()
    {
        PlaySound(NormalizeTimeSFX);

        for (float i = Time.timeScale; i < 1.05f; i += 0.05f)
        {
            Time.timeScale = i;
            //print("Time Scale: " + Time.timeScale);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    void PlaySound (AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}