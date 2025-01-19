using UnityEngine;

namespace Game.Inventory
{
    // == Grid ==

    public class PlaceInGridArgs
    {
        public string ItemId { get; set; }

        public string GridId { get; set; }
        public Vector3Int PositionOnGrid { get; set; }
    }

    // == Placement ==

    public class PlaceInPlacementArgs
    {
        public string ItemId { get; set; }
        public string PlacementId { get; set; }
    }

    public class RemoveFromPlacementArgs
    {
        public string ItemId { get; set; }
        public string PlacementId { get; set; }
    }

    public class TransferBetweenPlacementsArgs
    {
        public string ItemId { get; set; }

        public string SourcePlacementId { get; set; }
        public string TargetPlacementId { get; set; }
    }

    // == Slot ==

    public class PlaceInSlotArgs
    {
        public string ItemId { get; set; }
        public string SlotId { get; set; }
    }

    public class RemoveFromSlotArgs
    {
        public string ItemId { get; set; }
    }

    // == Split / Transfer ==

    public class TransferItemArgs
    {
        public string SourceItemId { get; set; }
        public string TargetItemId { get; set; }

        public int Count { get; set; }
    }

    public class SplittingItemArgs
    {
        public string SelectedItemId { get; set; }
        public bool IsRotated { get; set; }

        public string GridId { get; set; }
        public Vector3Int PositionOnGrid { get; set; }

        public int Count { get; set; }
    }

    // == Take ==

    public class TakeItemsArgs
    {
        public int ItemDataId { get; set; }
        public int Count { get; set; }

        public string GridId { get; set; }
    }

    // == Discard ==

    public class DiscardItemArgs
    {
        public string ItemId { get; set; }

        public string SlotId { get; set; }
        public string PlacementId { get; set; }
    }
}