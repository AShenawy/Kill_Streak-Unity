using UnityEngine;

namespace Enemies
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        public DifficultyManager DifficultyManager;
        public float RunSpeed = 7f;
        public bool IsActive = true;


        private void Start()
        {
            if (DifficultyManager == null)
                DifficultyManager = FindObjectOfType<DifficultyManager>();

            RunSpeed = DifficultyManager.speed;

            // Assign player as target if not assigned in Editor
            if (_target == null)
                _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        void LateUpdate ()
        {
            // Chase after target
            if (IsActive)
                transform.position = Vector3.MoveTowards(transform.position, _target.position, RunSpeed * Time.deltaTime);
        }
    }
}
