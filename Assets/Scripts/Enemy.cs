using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health = 3;
        [SerializeField] private float speed = 5f;
        [SerializeField] private int reward = 5;
        [SerializeField] private int damage = 1;

        private Path _path;
        private int _currentWaypointIndex = 0;
        private Vector2 _currentWaypoint;
        private Animator _animator;
        
        private static readonly int DeathAnimation = Animator.StringToHash("Death");
        
        public bool IsDying { get; private set; } = false;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Setup(Path path)
        {
            _path = path;
            _currentWaypoint = path[0].position;
        }

        private void Update()
        {
            if (IsDying)
                return;
            
            if (IsNearCurrentWaypoint())
            {
                transform.position = _currentWaypoint;
                GetNextWaypoint();
            }
            
            MoveToCurrentWaypoint();
        }

        private bool IsNearCurrentWaypoint()
        {
            float distanceToWaypoint = Vector2.Distance(transform.position, _currentWaypoint);
            return distanceToWaypoint < 0.01f;
        }
        
        private void GetNextWaypoint()
        {
            if (_currentWaypointIndex + 1 == _path.Waypoints.Length)
                return;
            
            ++_currentWaypointIndex;
            _currentWaypoint = _path[_currentWaypointIndex].position;
        }

        private void MoveToCurrentWaypoint()
        {
            float maxDistance = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,
                _currentWaypoint, maxDistance);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Finish"))
            {
                PlayerStats.Instance.Health -= damage;
                IsDying = true;
                Destroy(gameObject, 1);
            }
        }

        public void AddDamage(int count)
        {
            health -= count;
            if (health <= 0)
            {
                IsDying = true;
                PlayDeathAnimation();
            }
        }

        private void PlayDeathAnimation()
        {
            _animator.SetTrigger(DeathAnimation);
        }

        public void Death()
        {
            PlayerStats.Instance.Gems += reward;
            --EnemyController.Instance.AliveEnemies;
            Destroy(gameObject);
        }
    }
}