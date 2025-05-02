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
        private ConsumableTypesList typesList;
        private ConsumableMechanicHandlersList mechanicHandlersList;
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

            typesList = root.CreateElement<ConsumableTypesList>();
            typesList.BindProperty("types", GetData(data));
            typesList.FlexWidth(33).MarginRight(10);

            mechanicHandlersList = root.CreateElement<ConsumableMechanicHandlersList>();
            mechanicHandlersList.BindProperty("mechanicHandlers", GetData(data));
            mechanicHandlersList.FlexWidth(33).MarginRight(10);

            gridParamsEditor = root.CreateElement<GridParamsForItemsElement>();
            gridParamsEditor.BindProperty("gridParams", GetData(data));
            gridParamsEditor.FlexWidth(33);
        }
    }
}