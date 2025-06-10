using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [InventoryEditor(typeof(ItemSlotUIParams))]
    public class ItemSlotUIParamsEditor : Element
    {
        private AddressableElement<GameObject> itemInSlotRefField;
        private AddressableElement<GameObject> itemTooltipRefField;

        protected override void CreateElementGUI(Element root)
        {
            itemInSlotRefField = root.CreateAddressable<GameObject>();
            itemTooltipRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemInSlotRefField.BindProperty("itemInSlotRef", data);
            itemTooltipRefField.BindProperty("itemTooltipRef", data);
        }
    }
}