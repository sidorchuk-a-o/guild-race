using AD.ToolsCollection;
using UnityEngine;

namespace Game.Instances
{
    [InstancesEditor(typeof(ConsumableMechanicUIParams))]
    public class ItemSlotUIParamsEditor : Element
    {
        private AddressableElement<GameObject> containerRefField;

        protected override void CreateElementGUI(Element root)
        {
            containerRefField = root.CreateAddressable<GameObject>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            containerRefField.BindProperty("containerRef", data);
        }
    }
}