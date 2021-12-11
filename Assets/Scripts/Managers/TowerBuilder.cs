using System;
using System.Collections.Generic;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Managers
{
    public class TowerBuilder : MonoBehaviour
    {
        #region Singleton

            public static TowerBuilder Instance { get; private set; }

            private void Awake()
            {
                if (Instance != null)
                    throw new Exception("Tower builder already exists.");
                Instance = this;
            }

            private void OnDestroy()
            {
                Instance = null;
            }

        #endregion

        private TowerCard _selectedTower;
        public TowerCard SelectedTower
        {
            get => _selectedTower;
            set
            {
                if (_selectedTower)
                    _selectedTower.Deselect();
                
                _selectedTower = value;
            }
        }

        [SerializeField] private GridManager gridManager;

        public bool IsSellTowerState { get; set; } = false;
        
        private readonly Dictionary<Vector3Int, Tower> _builtTowers = new Dictionary<Vector3Int, Tower>();
        private readonly Vector3 _cellOffset = new Vector3(0.5f, 0.5f, 0.5f);
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)
                || EventSystem.current.IsPointerOverGameObject())
                return;
            
            Vector3Int clickedTilePos = gridManager.GetClickedTilePosition();
            
            if (IsSellTowerState)
            {
                if (_builtTowers.ContainsKey(clickedTilePos))
                    SellTower(_builtTowers[clickedTilePos], clickedTilePos);
            }
            else if (SelectedTower)
            {
                if (IsCellFree(clickedTilePos))
                {
                    if (_selectedTower.TowerPrefab.Price <= PlayerStats.Instance.Gems)
                        BuildTower(clickedTilePos);
                }
            }
        }
        
        private bool IsCellFree(Vector3Int position)
        {
            return gridManager.IsConstructionAllowed(position) 
                   && !_builtTowers.ContainsKey(position);
        }

        private void BuildTower(Vector3Int position)
        {
            PlayerStats.Instance.Gems -= _selectedTower.TowerPrefab.Price;
            Tower instance = Instantiate(_selectedTower.TowerPrefab, position + _cellOffset,
                Quaternion.identity, transform);
            _builtTowers.Add(position, instance);
        }

        private void SellTower(Tower tower, Vector3Int position)
        {
            PlayerStats.Instance.Gems += tower.Price / 2;
            _builtTowers.Remove(position);
            Destroy(tower.gameObject);
        }

        public void SwitchSellState()
        {
            if (IsSellTowerState)
                DisableSellState();
            else
                EnableSellState();
        }
        
        private void EnableSellState()
        {
            IsSellTowerState = true;
            if (_selectedTower)
            {
                _selectedTower.Deselect();
                _selectedTower = null;
            }
        }

        private void DisableSellState()
        {
            IsSellTowerState = false;
        }
    }
}