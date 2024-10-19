using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [Serializable]
    public class EquipSlot : Key
    {
        public EquipSlot()
        {
        }

        public EquipSlot(string key) : base(key)
        {
        }

        public static implicit operator string(EquipSlot key) => key?.value;
        public static implicit operator EquipSlot(string key) => new(key);
    }
}