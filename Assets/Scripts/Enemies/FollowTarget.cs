using System;
using UnityEngine;


public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    public DifficultyManager difficultyManager;
    public float runSpeed = 7f;
    public bool isActive = true;


    private void Start()
    {
        if (difficultyManager == null)
            difficultyManager = FindObjectOfType<DifficultyManager>();

        runSpeed = difficultyManager.speed;

        // Assign player as target if not assigned in Editor
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void LateUpdate ()
    {
        // Chase after target
        if (isActive)
        transform.position = Vector3.MoveTowards(transform.position, target.position, runSpeed * Time.deltaTime);
    }
}
