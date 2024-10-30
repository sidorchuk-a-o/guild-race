using UnityEngine;

namespace Game.Inventory
{
    public class EquipSlotData : ItemSlotData
    {
        [SerializeField] private EquipGroup equipGroup;

        public EquipGroup EquipGroup => equipGroup;
    }
}