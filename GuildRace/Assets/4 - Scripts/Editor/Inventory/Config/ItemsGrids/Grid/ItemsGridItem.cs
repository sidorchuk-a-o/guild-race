using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemsGridData"/>
    /// </summary>
    public class ItemsGridItem : ListItemElement
    {
        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();

            rowSizeField = root.CreateProperty();
            rowSizeField.Width(80);
            rowSizeField.labelOn = false;

            rowCountField = root.CreateProperty();
            rowCountField.Width(80);
            rowCountField.labelOn = false;

            categoryField = root.CreateKey<ItemsGridCategory, int>();
            categoryField.Width(160);
            categoryField.filterOn = false;
            categoryField.removeOn = false;
            categoryField.labelOn = false;

            cellTypeField = root.CreateKey<ItemsGridCellType, int>();
            cellTypeField.FlexGrow(1);
            cellTypeField.filterOn = false;
            cellTypeField.removeOn = false;
            cellTypeField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            rowSizeField.BindProperty("rowSize", data);
            rowCountField.BindProperty("rowCount", data);
            categoryField.BindProperty("category", data);
            cellTypeField.BindProperty("cellType", data);
        }
    }
}