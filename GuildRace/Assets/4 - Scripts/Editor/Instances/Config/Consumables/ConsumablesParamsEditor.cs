using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="ConsumablesParams"/>
    /// </summary>
    public class ConsumablesParamsEditor
    {
        private ConsumablesItemsList itemsList;
        private GridParamsForItemsElement gridParamsEditor;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("consumablesParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Items", CreateItemsTab);
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateItemsTab(VisualElement root, SerializedData data)
        {
            itemsList = root.CreateElement<ConsumablesItemsList>();
            itemsList.BindProperty("items", GetData(data));
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            gridParamsEditor = root.CreateElement<GridParamsForItemsElement>();
            gridParamsEditor.BindProperty("gridParams", GetData(data));
            gridParamsEditor.FlexWidth(33);
        }
    }
}