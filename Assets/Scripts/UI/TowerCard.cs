using TowerDefense.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class TowerCard : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Tower towerPrefab;
        [SerializeField] private Image turretSprite;
        [SerializeField] private TowerCardParameters parameters;
        
        [Header("Colors")] 
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;

        public Tower TowerPrefab => towerPrefab;
        
        private bool _isSelected = false;

        private void Awake()
        {
            background.color = defaultColor;
            turretSprite.color = towerPrefab.SpriteColor;
            parameters.Initialize(towerPrefab);
        }

        public void OnClick()
        {
            if (TowerBuilder.Instance.IsSellTowerState)
                return;
            
            if (!_isSelected)
            {
                TowerBuilder.Instance.SelectedTower = this;
                Select();
            }
            else
            {
                TowerBuilder.Instance.SelectedTower = null;
                Deselect();
            }
        }

        public void Select()
        {
            _isSelected = true;
            background.color = selectedColor;
        }
        
        public void Deselect()
        {
            _isSelected = false;
            background.color = defaultColor;
        }
    }
}