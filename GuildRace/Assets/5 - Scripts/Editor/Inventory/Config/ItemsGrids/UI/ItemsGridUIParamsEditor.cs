using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    [CustomEditor(typeof(ItemsGridUIParams))]
    public class ItemsGridUIParamsEditor : MonoEditor
    {
        private PropertyElement cellSizeField;
        private ItemGridUIParamsList parametersList;

        public override void CreateGUI(VisualElement root)
        {
            cellSizeField = root.CreateProperty();
            parametersList = root.CreateElement<ItemGridUIParamsList>();
        }

        protected override void BindData(SerializedData data)
        {
            cellSizeField.BindProperty("cellSize", data);
            parametersList.BindProperty("parameters", data);
        }
    }
}