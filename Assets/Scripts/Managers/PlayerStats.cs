using System;
using TMPro;
using UnityEngine;

namespace TowerDefense.Managers
{
    public class PlayerStats : MonoBehaviour
    {
        #region Singleton

            public static PlayerStats Instance { get; private set; }

            private void Awake()
            {
                if (Instance != null)
                    throw new Exception("PlayerStats already exists.");
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        [Header("UI Fields")] [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Parameters")] [SerializeField]
        private int gems = 100;

        [SerializeField] private int health = 100;

        public delegate void NoMoreHealthHandler();

        public event NoMoreHealthHandler NoMoreHealthEvent;

        public int Gems
        {
            get => gems;
            set
            {
                gems = value;
                gemsText.text = gems.ToString();
            }
        }

        public int Health
        {
            get => health;
            set
            {
                health = value;
                healthText.text = health.ToString();

                if (health <= 0)
                {
                    NoMoreHealthEvent?.Invoke();
                }
            }
        }

        private void Start()
        {
            gemsText.text = gems.ToString();
            healthText.text = health.ToString();
        }
    }
}