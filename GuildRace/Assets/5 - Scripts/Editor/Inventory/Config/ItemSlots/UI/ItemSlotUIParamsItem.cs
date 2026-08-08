using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class ItemSlotUIParamsItem : ListItemElement<ItemSlotUIParams>
    {
        private KeyElement<int> itemTypeField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            itemTypeField = root.CreateKeyPopup<ItemType, int>();
            itemTypeField.FlexGrow(1).ReadOnly();
            itemTypeField.FilterOn = false;
            itemTypeField.RemoveOn = false;
            itemTypeField.UpdateOn = false;
            itemTypeField.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            FoldoutOn = true;

            itemTypeField.BindProperty("itemType", data);
        }
    }
}