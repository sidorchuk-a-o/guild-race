using AD.ToolsCollection;
using UnityEngine;

namespace Game.Instances
{
    [InstancesEditor(typeof(RewardMechanicUIParams))]
    public class RewardMechanicUIParamsEditor : Element
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