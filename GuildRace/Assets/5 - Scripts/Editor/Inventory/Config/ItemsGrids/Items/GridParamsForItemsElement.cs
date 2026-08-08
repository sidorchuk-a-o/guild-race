using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    /// <summary>
    ///     Element: <see cref="GridParamsForItems" />
    /// </summary>
    public class GridParamsForItemsElement : Element
    {
        private ItemsGridCategoryKeysList categoriesList;
        private ItemsGridCellTypeKeysList cellTypesList;

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Grid Params For Items");

            categoriesList = root.CreateElement<ItemsGridCategoryKeysList>();
            categoriesList.FlexGrow(0);

            cellTypesList = root.CreateElement<ItemsGridCellTypeKeysList>();
            cellTypesList.FlexGrow(0);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            categoriesList.BindProperty("categories", data);
            cellTypesList.BindProperty("cellTypes", data);
        }
    }
}