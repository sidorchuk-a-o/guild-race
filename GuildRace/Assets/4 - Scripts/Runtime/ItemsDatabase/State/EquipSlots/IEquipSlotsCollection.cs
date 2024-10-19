using AD.States;

namespace Game.Items
{
    public interface IEquipSlotsCollection : IReadOnlyReactiveCollectionInfo<EquipSlotInfo>
    {
        EquipSlotInfo this[EquipSlot slot] { get; }
    }
}