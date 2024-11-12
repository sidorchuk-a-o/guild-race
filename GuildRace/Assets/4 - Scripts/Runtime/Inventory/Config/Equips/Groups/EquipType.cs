using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class EquipType : Key<int>
    {
        public EquipType()
        {
        }

        public EquipType(int key) : base(key)
        {
        }

        public static implicit operator int(EquipType key) => key?.value ?? -1;
        public static implicit operator EquipType(int key) => new(key);
    }
}