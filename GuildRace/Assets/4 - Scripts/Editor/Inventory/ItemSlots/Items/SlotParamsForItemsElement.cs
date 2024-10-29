using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Element: <see cref="SlotParamsForItems"/>
    /// </summary>
    public class SlotParamsForItemsElement : Element
    {
        private ItemSlotKeysList slotsList;

        protected override void CreateElementGUI(Element root)
        {
            root.CreateHeader("Slot Params For Items");

            slotsList = root.CreateElement<ItemSlotKeysList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            slotsList.BindProperty("slots", data);
        }
    }
}