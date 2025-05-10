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
        private ItemsGridsFactoriesList factoriesList;

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
            root.ConvertToRow();

            categoriesList = root.CreateElement<ItemsGridCategoriesList>();
            categoriesList.BindProperty("categories", GetData(data));
            categoriesList.FlexWidth(33).MarginRight(10);

            cellTypesList = root.CreateElement<ItemsGridCellTypesList>();
            cellTypesList.BindProperty("cellTypes", GetData(data));
            cellTypesList.FlexWidth(33).MarginRight(10);

            factoriesList = root.CreateElement<ItemsGridsFactoriesList>();
            factoriesList.BindProperty("factories", GetData(data));
            factoriesList.FlexWidth(33);
        }
    }
}