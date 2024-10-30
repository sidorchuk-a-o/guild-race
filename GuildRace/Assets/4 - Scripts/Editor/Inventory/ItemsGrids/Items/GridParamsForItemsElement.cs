using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Element: <see cref="GridParamsForItems"/>
    /// </summary>
    public class GridParamsForItemsElement : Element
    {
        private ItemsGridCategoryKeysList categoriesList;
        private ItemsGridCellTypeKeysList cellTypesList;

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Grid Params For Items");

            categoriesList = root.CreateElement<ItemsGridCategoryKeysList>();
            cellTypesList = root.CreateElement<ItemsGridCellTypeKeysList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            categoriesList.BindProperty("categories", data);
            cellTypesList.BindProperty("cellTypes", data);
        }
    }
}