using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [InventoryEditor(typeof(ItemTooltipUIParams))]
    public class ItemTooltipUIParamsEditor : Element
    {
        private AddressableElement<GameObject> tooltipRefField;

        protected override void CreateElementGUI(Element root)
        {
            tooltipRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            tooltipRefField.BindProperty("tooltipRef", data);
        }
    }
}