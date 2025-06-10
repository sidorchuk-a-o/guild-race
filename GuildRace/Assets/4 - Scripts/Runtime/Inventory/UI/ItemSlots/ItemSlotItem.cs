using AD.UI;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemSlotItem : MonoBehaviour
    {
        [SerializeField] private UIText nameText;

        public void Init(ItemSlotDataVM slotVM)
        {
            nameText.SetTextParams(slotVM.NameKey);
        }
    }
}