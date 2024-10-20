using AD.States;
using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game.Items
{
    public class EquipSlotsCollection : ReactiveCollectionInfo<EquipSlotInfo>, IEquipSlotsCollection
    {
        private readonly ReactiveProperty<int> itemsLevel = new();

        public EquipSlotInfo this[EquipSlot slot] => Values.FirstOrDefault(x => x.Slot == slot);
        public IReadOnlyReactiveProperty<int> ItemsLevel => itemsLevel;

        public EquipSlotsCollection(IEnumerable<EquipSlotInfo> values) : base(values)
        {
        }

        public void Init()
        {
            foreach (var slot in Values)
            {
                slot.Item.SilentSubscribe(UpdateItemsLevel);
            }

            UpdateItemsLevel();
        }

        private void UpdateItemsLevel()
        {
            var count = 0;
            var level = 0;

            foreach (var slot in Values)
            {
                if (slot.HasItem)
                {
                    count++;
                    level += slot.Item.Value.Level;
                }
            }

            itemsLevel.Value = level / Mathf.Max(1, count);
        }
    }
}