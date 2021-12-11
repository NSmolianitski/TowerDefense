using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

            public static GameManager Instance { get; private set; }
            
            private void Awake()
            {
                if (Instance != null)
                    throw new Exception("GameManager already exists.");
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        [Header("UI screens")]
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject winScreen;

        [Header("Other")] 
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private Camera cam;
        public Camera Camera => cam;
        
        private void Start()
        {
            playerStats.NoMoreHealthEvent += OnNoMoreHealthEvent;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }

        public void Pause()
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        public void LaunchVictory()
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }

        public void OnNoMoreHealthEvent()
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }

        public void RestartLevel()
        {
            Time.timeScale = 1;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}