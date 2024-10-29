using UnityEngine;

namespace Game.Inventory
{
    public class PickupResult
    {
        public ItemVM SelectedItem { get; set; }

        public BoundsInt Bounds { get; set; }
        public bool IsRotated { get; set; }

        public PickupContext Context { get; set; }

        public bool ThisPositionIsPositionOfSelectedItem(in PositionOnGrid positionOnGrid, ItemsGridContainer selectedGrid)
        {
            var sameGrid = Context.SelectedGrid == selectedGrid;
            var samePosition = Bounds.Contains2D(positionOnGrid.Cursor);

            return sameGrid && samePosition;
        }
    }
}