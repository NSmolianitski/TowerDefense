using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense.Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<TileData> tileDatas;
        
        private readonly Dictionary<TileBase, TileData> _dataFromTiles = new Dictionary<TileBase, TileData>();

        private void Awake()
        {
            FillDataFromTilesDictionary();
        }

        private void FillDataFromTilesDictionary()
        {
            foreach (var tileData in tileDatas)
            {
                foreach (var tile in tileData.Tiles)
                {
                    _dataFromTiles.Add(tile, tileData);
                }
            }
        }

        public bool IsConstructionAllowed(Vector3Int position)
        {
            TileBase tileBase = tilemap.GetTile(position);
            TileData tileData = _dataFromTiles[tileBase];
            
            return tileData.IsConstructionAllowed;
        }
        
        public Vector3Int GetClickedTilePosition()
        {
            var clickedPos = GameManager.Instance.Camera.ScreenToWorldPoint(Input.mousePosition);
            return tilemap.WorldToCell(clickedPos);
        }
    }
}