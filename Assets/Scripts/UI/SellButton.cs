using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class SellButton : MonoBehaviour
    {
        [SerializeField] private Color sellStateColor;
        
        private Color _defaultColor;
        private Image _image;
        private bool _isSellStateActive = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
        }

        public void SwitchSellState()
        {
            if (_isSellStateActive)
                DisableSellState();
            else
                EnableSellState();

            _isSellStateActive = !_isSellStateActive;
        }
        
        private void EnableSellState()
        {
            _image.color = sellStateColor;
        }

        private void DisableSellState()
        {
            _image.color = _defaultColor;
        }
    }
}