using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class EnemiesGroup : ScriptableObject
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private int count = 10;

        public Enemy EnemyPrefab => enemyPrefab;
        public int Count => count;
    }
}