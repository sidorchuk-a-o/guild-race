using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemsGridsParams"/>
    /// </summary>
    public class ItemsGridsParamsEditor
    {
        private ItemsGridCategoriesList categoriesList;
        private ItemsGridCellTypesList cellTypesList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("itemsGridsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            categoriesList = root.CreateElement<ItemsGridCategoriesList>();
            categoriesList.BindProperty("categories", GetData(data));
            categoriesList.MaxWidth(33, LengthUnit.Percent);

            cellTypesList = root.CreateElement<ItemsGridCellTypesList>();
            cellTypesList.BindProperty("cellTypes", GetData(data));
            cellTypesList.MaxWidth(33, LengthUnit.Percent);
        }
    }
}