using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Instances
{
    [DataEditor(typeof(ConsumableMechanicUIParams))]
    public class ConsumableMechanicUIParamsEditor : Element
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