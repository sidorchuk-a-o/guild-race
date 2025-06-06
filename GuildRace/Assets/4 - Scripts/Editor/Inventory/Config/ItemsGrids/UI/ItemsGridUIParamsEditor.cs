using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [UnityEditor.CustomEditor(typeof(ItemsGridUIParams))]
    public class ItemsGridUIParamsEditor : MonoEditor
    {
        private PropertyElement cellSizeField;
        private ItemGridUIParamsList itemsParamsList;

        public override void CreateGUI(VisualElement root)
        {
            cellSizeField = root.CreateProperty();
            itemsParamsList = root.CreateElement<ItemGridUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            cellSizeField.BindProperty("cellSize", data);
            itemsParamsList.BindProperty("itemsParams", data);
        }
    }
}