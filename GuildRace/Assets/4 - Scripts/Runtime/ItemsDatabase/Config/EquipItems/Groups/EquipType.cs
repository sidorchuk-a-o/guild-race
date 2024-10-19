using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [Serializable]
    public class EquipType : Key
    {
        public EquipType()
        {
        }

        public EquipType(string key) : base(key)
        {
        }

        public static implicit operator string(EquipType key) => key?.value;
        public static implicit operator EquipType(string key) => new(key);
    }
}