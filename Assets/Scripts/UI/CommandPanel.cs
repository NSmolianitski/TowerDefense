using TowerDefense.Managers;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class CommandPanel : MonoBehaviour
    {
        [SerializeField] private Button nextWaveButton;
        [SerializeField] private Button sellButton;

        private void Start()
        {
            EnemyController.Instance.WaveEndedEvent += ActivateNextWaveButton;
        }

        private void ActivateNextWaveButton()
        {
            nextWaveButton.interactable = true;
        }
        
        public void OnNextWaveButtonClicked()
        {
            nextWaveButton.interactable = false;
            EnemyController.Instance.LaunchNextWave();
        }

        public void OnSellButtonClicked()
        {
            TowerBuilder.Instance.SwitchSellState();
            sellButton.GetComponent<SellButton>().SwitchSellState();
        }
    }
}