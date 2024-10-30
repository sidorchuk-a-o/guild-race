using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class EquipGroup : Key
    {
        public EquipGroup()
        {
        }

        public EquipGroup(string key) : base(key)
        {
        }

        public static implicit operator string(EquipGroup key) => key?.value;
        public static implicit operator EquipGroup(string key) => new(key);
    }
}