using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Items
{
    /// <summary>
    /// Editor: <see cref="EquipsParams"/>
    /// </summary>
    public class EquipsParamsEditor
    {
        private EquipItemsList itemsList;
        private EquipSlotsList slotsList;
        private EquipGroupsList groupsList;
        private RaritiesList raritiesList;

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
            slotsList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);

            groupsList = root.CreateElement<EquipGroupsList>();
            groupsList.BindProperty("groups", GetData(data));
            groupsList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);

            raritiesList = root.CreateElement<RaritiesList>();
            raritiesList.BindProperty("rarities", GetData(data));
            raritiesList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent);
        }
    }
}