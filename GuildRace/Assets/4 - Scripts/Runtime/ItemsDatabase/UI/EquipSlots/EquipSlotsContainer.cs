using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    public class EquipSlotsContainer : MonoBehaviour
    {
        [Header("Slot")]
        [SerializeField] private List<EquipSlotItem> equipSlots;

        public void Init(EquipSlotsVM equipSlotsVM, CompositeDisp disp)
        {
            for (var i = 0; i < equipSlots.Count; i++)
            {
                var slot = equipSlots[i];
                var slotVM = equipSlotsVM[i];

                slot.Init(slotVM, disp);
            }
        }
    }
}