using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    [DataEditor(typeof(ItemGridUIParams))]
    public class ItemGridUIParamsEditor : Element
    {
        private AddressableElement<GameObject> itemInGridRefField;

        protected override void CreateElementGUI(Element root)
        {
            itemInGridRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemInGridRefField.BindProperty("itemInGridRef", data);
        }
    }
}