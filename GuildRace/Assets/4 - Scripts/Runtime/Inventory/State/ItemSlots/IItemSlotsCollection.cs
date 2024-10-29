using AD.States;

namespace Game.Inventory
{
    public interface IItemSlotsCollection : IReadOnlyReactiveCollectionInfo<ItemSlotInfo>
    {
        ItemSlotInfo this[ItemSlot slot] { get; }
    }
}