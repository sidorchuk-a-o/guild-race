using UnityEngine;

namespace Game.Inventory
{
    public class PickupResult
    {
        public ItemVM SelectedItemVM { get; set; }

        public BoundsInt Bounds { get; set; }
        public bool IsRotated { get; set; }

        public PickupContext Context { get; set; }

        public bool ThisPositionIsPositionOfSelectedItem(in PositionOnGrid positionOnGrid, ItemsGridVM selectedGridVM)
        {
            var sameGrid = Context.SelectedGridVM == selectedGridVM;
            var samePosition = Bounds.Contains2D(positionOnGrid.Cursor);

            return sameGrid && samePosition;
        }
    }
}