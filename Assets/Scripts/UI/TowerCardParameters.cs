using TMPro;
using UnityEngine;

namespace TowerDefense.UI
{
    public class TowerCardParameters : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private TextMeshProUGUI shootRateText;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI rangeText;

        public void Initialize(Tower tower)
        {
            costText.text = tower.Price.ToString();
            shootRateText.text = tower.ShootRate.ToString();
            damageText.text = tower.Damage.ToString();
            rangeText.text = tower.Range.ToString();
        }
    }
}