using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    /// <summary>
    ///     Editor: <see cref="ItemsGridData" />
    /// </summary>
    public abstract class ItemsGridEditor : DataEditor
    {
        private PropertyElement rowSizeField;
        private PropertyElement rowCountField;
        private KeyElement<int> categoryField;
        private KeyElement<int> cellTypeField;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateCommonTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        protected virtual void CreateCommonTab(VisualElement root, SerializedData data)
        {
            rowSizeField = root.CreateProperty();
            rowSizeField.BindProperty("rowSize", data);

            rowCountField = root.CreateProperty();
            rowCountField.BindProperty("rowCount", data);

            root.CreateSpace();
            categoryField = root.CreateKeyPopup<ItemsGridCategory, int>();
            categoryField.BindProperty("category", data);

            cellTypeField = root.CreateKeyPopup<ItemsGridCellType, int>();
            cellTypeField.BindProperty("cellType", data);
        }
    }
}