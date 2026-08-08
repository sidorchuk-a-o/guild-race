using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public interface IItemSlotsCollection : IReadOnlyReactiveCollection<ItemSlotInfo>
    {
        ItemSlotInfo this[ItemSlot slot] { get; }
    }
}