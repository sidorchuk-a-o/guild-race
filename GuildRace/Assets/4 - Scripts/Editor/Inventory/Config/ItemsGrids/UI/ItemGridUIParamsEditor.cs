using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [InventoryEditor(typeof(ItemGridUIParams))]
    public class ItemGridUIParamsEditor : Element
    {
        private AddressableElement<GameObject> itemInGridRefField;
        private AddressableElement<GameObject> itemTooltipRefField;

        protected override void CreateElementGUI(Element root)
        {
            itemInGridRefField = root.CreateAddressable<GameObject>();
            itemTooltipRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemInGridRefField.BindProperty("itemInGridRef", data);
            itemTooltipRefField.BindProperty("itemTooltipRef", data);
        }
    }
}