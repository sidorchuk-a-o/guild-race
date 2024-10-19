using AD.ToolsCollection;
using UniRx;

namespace Game.Items
{
    public class EquipSlotInfo
    {
        private readonly ReactiveProperty<EquipItemInfo> item = new();

        public EquipSlot Slot { get; }

        public bool HasItem => item.IsValid();
        public IReadOnlyReactiveProperty<EquipItemInfo> Item => item;

        public EquipSlotInfo(EquipSlot slot)
        {
            Slot = slot;
        }

        public void SetItem(EquipItemInfo value)
        {
            item.Value = value;
        }
    }
}