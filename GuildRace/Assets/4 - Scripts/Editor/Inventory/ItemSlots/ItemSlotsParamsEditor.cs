using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemSlotsParams"/>
    /// </summary>
    public class ItemSlotsParamsEditor
    {
        private ItemSlotsList slotsList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("itemSlotsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Slots", CreateSlotsTab);
        }

        private void CreateSlotsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            slotsList = root.CreateElement<ItemSlotsList>();
            slotsList.BindProperty("slots", GetData(data));
            slotsList.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);
        }
    }
}