using System.Collections.Generic;
using TowerDefense.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Color spriteColor;
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Parameters")] 
        [SerializeField] private int price = 10;
        [SerializeField] private float shootRate = 10f;
        [SerializeField] private float projectileSpeed = 5f;
        [SerializeField] private int damage = 1;
        [SerializeField] private float range = 1f;

        public Color SpriteColor => spriteColor;
        public int Price => price;
        public float ShootRate => shootRate;
        public int Damage => damage;
        public float Range => range;
        
        private Enemy _target;
        private List<Transform> _enemiesInRange = new List<Transform>();
        private float _shootCooldown;

        private void Awake()
        {
            GetComponent<CircleCollider2D>().radius = range;
            spriteRenderer.color = spriteColor;
        }

        private void Update()
        {
            UpdateShootCooldown();
            ChooseTarget();
            
            if (_target == null || _shootCooldown > 0)
                return;
            
            Shoot();
        }

        private void UpdateShootCooldown()
        {
            if (_shootCooldown > 0)
                _shootCooldown -= Time.deltaTime;
        }
        
        private void ChooseTarget()
        {
            if (_target == null || _target.IsDying || !IsTargetInRange())
                _target = GetNewTarget();
        }

        private bool IsTargetInRange()
        {
            return Vector2.Distance(transform.position, _target.transform.position) <= range;
        }

        private Enemy GetNewTarget()
        {
            if (_enemiesInRange.Count == 0)
                return null;
            
            float distance = Vector2.Distance(transform.position, _enemiesInRange[0].position);
            float shortestDistance = distance;
            int shortestDistanceIndex = 0;
            
            for (int i = 1; i < _enemiesInRange.Count; ++i)
            {
                distance = Vector2.Distance(transform.position, _enemiesInRange[i].position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    shortestDistanceIndex = i;
                }
            }
            return _enemiesInRange[shortestDistanceIndex].GetComponent<Enemy>();
        }

        private void Shoot()
        {
            var instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
            instance.GetComponent<Projectile>().Init(_target, projectileSpeed, damage);
            _shootCooldown = shootRate;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
                _enemiesInRange.Add(other.transform);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
                _enemiesInRange.Remove(other.transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}