using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    [CustomEditor(typeof(ItemsTooltipUIParams))]
    public class ItemsTooltipUIParamsEditor : MonoEditor
    {
        private ItemTooltipUIParamsList parametersList;

        public override void CreateGUI(VisualElement root)
        {
            parametersList = root.CreateElement<ItemTooltipUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            parametersList.BindProperty("parameters", data);
        }
    }
}