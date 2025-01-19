using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="EquipsParams"/>
    /// </summary>
    public class EquipsParamsEditor
    {
        private EquipItemsList itemsList;
        private EquipSlotsList slotsList;
        private EquipGroupsList groupsList;
        private GridParamsForItemsElement gridParamsEditor;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("equipsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Items", CreateItemsTab);
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateItemsTab(VisualElement root, SerializedData data)
        {
            itemsList = root.CreateElement<EquipItemsList>();
            itemsList.BindProperty("items", GetData(data));
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            slotsList = root.CreateElement<EquipSlotsList>();
            slotsList.BindProperty("slots", GetData(data));
            slotsList.FlexWidth(33).MarginRight(10);

            groupsList = root.CreateElement<EquipGroupsList>();
            groupsList.BindProperty("groups", GetData(data));
            groupsList.FlexWidth(33).MarginRight(10);

            gridParamsEditor = root.CreateElement<GridParamsForItemsElement>();
            gridParamsEditor.BindProperty("gridParams", GetData(data));
            gridParamsEditor.FlexWidth(33);
        }
    }
}