using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    public abstract class ItemsGridData : ScriptableEntity<int>
    {
        // Size
        [SerializeField] private int rowSize;
        [SerializeField] private int rowCount;
        // Types
        [SerializeField] private ItemsGridCategory category;
        [SerializeField] private ItemsGridCellType cellType;

        public int RowSize => rowSize;
        public int RowCount => rowCount;
        public ItemsGridCategory Category => category;
        public ItemsGridCellType CellType => cellType;
    }
}