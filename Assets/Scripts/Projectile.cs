using UnityEngine;

namespace TowerDefense
{
    public class Projectile : MonoBehaviour
    {
        private Enemy _target;
        private float _speed;
        private int _damage;

        public void Init(Enemy target, float speed, int damage)
        {
            _target = target;
            _speed = speed;
            _damage = damage;
        }
        
        private void Update()
        {
            if (_target.IsDying)
                Destroy(gameObject);

            transform.position = Vector2.MoveTowards(transform.position, 
                _target.transform.position, _speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform == _target.transform)
            {
                other.GetComponent<Enemy>().AddDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}