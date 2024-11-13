using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class EquipGroup : Key<int>
    {
        public EquipGroup()
        {
        }

        public EquipGroup(int key) : base(key)
        {
        }

        public static implicit operator int(EquipGroup key) => key?.value ?? -1;
        public static implicit operator EquipGroup(int key) => new(key);
    }
}