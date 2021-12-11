using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class Wave : ScriptableObject
    {
        [SerializeField] private EnemiesGroup[] groups;
        [SerializeField] private float spawnRate = 0.2f;
        [SerializeField] private float timeBetweenGroups = 0.2f;

        public EnemiesGroup[] Groups => groups;
        public float SpawnRate => spawnRate;
        public float TimeBetweenGroups => timeBetweenGroups;

        public float CountWaveSpawnTime()
        {
            float result = (timeBetweenGroups * groups.Length) + spawnRate;
            return result;
        }
    }
}