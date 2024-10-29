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
        private EquipGroupsList groupsList;
        private SlotParamsForItemsElement slotParamsEditor;
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

            groupsList = root.CreateElement<EquipGroupsList>();
            groupsList.BindProperty("groups", GetData(data));
            groupsList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);

            slotParamsEditor = root.CreateElement<SlotParamsForItemsElement>();
            slotParamsEditor.BindProperty("slotParams", GetData(data));
            slotParamsEditor.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);

            gridParamsEditor = root.CreateElement<GridParamsForItemsElement>();
            gridParamsEditor.BindProperty("gridParams", GetData(data));
            gridParamsEditor.FlexGrow(1).MaxWidth(33, LengthUnit.Percent);
        }
    }
}