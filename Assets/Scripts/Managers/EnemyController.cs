using System;
using System.Collections;
using UnityEngine;

namespace TowerDefense.Managers
{
    public class EnemyController : MonoBehaviour
    {
        #region Singleton

            public static EnemyController Instance { get; private set; }
                
            private void Awake()
            {
                if (Instance != null)
                    throw new Exception("EnemyController already exists.");
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        [SerializeField] private Path path;
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Wave[] waves;

        public delegate void WaveEndedHandler();
        public event WaveEndedHandler WaveEndedEvent;
        
        public int AliveEnemies { get; set; } = 0;
        
        private int _currentWaveNumber = 0;

        private void Update()
        {
            if (_currentWaveNumber == waves.Length && AliveEnemies == 0)
            {
                GameManager.Instance.LaunchVictory();
                gameObject.SetActive(false);
            }
            else if (AliveEnemies == 0)
                WaveEndedEvent?.Invoke();
        }

        public void LaunchNextWave()
        {
            Wave newWave = waves[_currentWaveNumber];
            StartCoroutine(SpawnWave(newWave));
            ++_currentWaveNumber;
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            float waveSpawnTime = wave.CountWaveSpawnTime() + 1f;
            foreach (var group in wave.Groups)
            {
                StartCoroutine(SpawnGroup(group));
                yield return new WaitForSeconds(waveSpawnTime);
            }
        }

        private IEnumerator SpawnGroup(EnemiesGroup enemiesGroup)
        {
            for (int i = 0; i < enemiesGroup.Count; ++i)
            {
                SpawnEnemy(enemiesGroup.EnemyPrefab);
                yield return new WaitForSeconds(spawnRate);
            }
        }
        
        private void SpawnEnemy(Enemy enemyPrefab)
        {
            Enemy instance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);
            instance.Setup(path);
            ++AliveEnemies;
        }
    }
}