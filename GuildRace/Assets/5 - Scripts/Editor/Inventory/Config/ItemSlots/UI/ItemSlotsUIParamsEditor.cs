using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    [CustomEditor(typeof(ItemSlotsUIParams))]
    public class ItemSlotsUIParamsEditor : MonoEditor
    {
        private ItemSlotUIParamsList parametersList;

        public override void CreateGUI(VisualElement root)
        {
            parametersList = root.CreateElement<ItemSlotUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            parametersList.BindProperty("parameters", data);
        }
    }
}