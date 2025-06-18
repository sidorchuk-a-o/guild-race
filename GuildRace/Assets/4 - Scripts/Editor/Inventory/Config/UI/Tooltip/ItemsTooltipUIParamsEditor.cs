using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [UnityEditor.CustomEditor(typeof(ItemsTooltipUIParams))]
    public class ItemsTooltipUIParamsEditor : MonoEditor
    {
        private ItemTooltipUIParamsList parametersList;

        public override void CreateGUI(VisualElement root)
        {
            parametersList = root.CreateElement<ItemTooltipUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            parametersList.BindProperty("parameters", data);
        }
    }
}