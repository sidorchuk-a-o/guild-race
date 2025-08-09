using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Store
{
    [StoreEditor(typeof(RewardResultUIParams))]
    public class RewardResultUIParamsEditor : Element
    {
        private AddressableElement<GameObject> itemRefField;

        protected override void CreateElementGUI(Element root)
        {
            itemRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemRefField.BindProperty("itemRef", data);
        }
    }
}