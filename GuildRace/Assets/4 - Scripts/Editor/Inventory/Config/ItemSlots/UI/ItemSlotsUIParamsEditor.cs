using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [UnityEditor.CustomEditor(typeof(ItemSlotsUIParams))]
    public class ItemSlotsUIParamsEditor : MonoEditor
    {
        private ItemSlotUIParamsList parametersList;

        public override void CreateGUI(VisualElement root)
        {
            parametersList = root.CreateElement<ItemSlotUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            parametersList.BindProperty("parameters", data);
        }
    }
}