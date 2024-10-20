using AD.States;
using UniRx;

namespace Game.Items
{
    public interface IEquipSlotsCollection : IReadOnlyReactiveCollectionInfo<EquipSlotInfo>
    {
        EquipSlotInfo this[EquipSlot slot] { get; }

        IReadOnlyReactiveProperty<int> ItemsLevel { get; }
    }
}