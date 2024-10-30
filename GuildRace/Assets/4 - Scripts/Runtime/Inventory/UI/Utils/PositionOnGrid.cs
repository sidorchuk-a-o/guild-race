using UnityEngine;

namespace Game.Inventory
{
    public struct PositionOnGrid
    {
        public Vector3Int Item { get; set; }
        public Vector3Int Cursor { get; set; }
    }
}