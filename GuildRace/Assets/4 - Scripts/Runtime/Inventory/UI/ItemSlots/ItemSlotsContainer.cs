using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemSlotsContainer : MonoBehaviour
    {
        [Header("Slots")]
        [SerializeField] private List<ItemSlotContainer> itemSlots;

        public void Init(ItemSlotsVM itemSlotsVM, CompositeDisp disp)
        {
            foreach (var slot in itemSlots)
            {
                var slotVM = itemSlotsVM[slot.Slot];

                slot.Init(slotVM, disp);
            }
        }
    }
}