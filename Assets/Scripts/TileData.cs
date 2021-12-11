using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TileData : ScriptableObject
    {
        [SerializeField] private TileBase[] tiles;
        public TileBase[] Tiles => tiles;

        [SerializeField] private bool isConstructionAllowed = true;
        public bool IsConstructionAllowed => isConstructionAllowed;
    }
}
