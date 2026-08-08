using UnityEngine;

namespace AD.Game.Inventory
{
    public class EquipSlotData : ItemSlotData
    {
        [SerializeField] private EquipGroup equipGroup;

        public EquipGroup EquipGroup => equipGroup;
    }
}