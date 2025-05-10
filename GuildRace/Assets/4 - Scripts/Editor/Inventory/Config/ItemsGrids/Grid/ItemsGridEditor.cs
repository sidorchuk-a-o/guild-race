using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemsGridData"/>
    /// </summary>
    public abstract class ItemsGridEditor : EntityEditor
    {
        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateCommonTab);
            tabs.content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            rowSizeField = root.CreateProperty();
            rowSizeField.BindProperty("rowSize", data);

            rowCountField = root.CreateProperty();
            rowCountField.BindProperty("rowCount", data);

            root.CreateSpace();
            categoryField = root.CreateKey<ItemsGridCategory, int>();
            categoryField.BindProperty("category", data);

            cellTypeField = root.CreateKey<ItemsGridCellType, int>();
            cellTypeField.BindProperty("cellType", data);
        }
    }
}