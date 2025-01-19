using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemSlotsParams"/>
    /// </summary>
    public class ItemSlotsParamsEditor
    {
        private ItemSlotsFactoriesList factoriesList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("itemSlotsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Params", CreateSlotsTab);
        }

        private void CreateSlotsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            factoriesList = root.CreateElement<ItemSlotsFactoriesList>();
            factoriesList.BindProperty("factories", GetData(data));
            factoriesList.FlexWidth(33).MarginRight(10);
        }
    }
}