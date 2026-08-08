using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class ItemSlotKeyItem : ListItemElement<ItemSlot>
    {
        private KeyElement<int> keyField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            keyField = root.CreateKeyPopup<ItemSlot, int>();
            keyField.FlexGrow(1);
            keyField.RemoveOn = false;
            keyField.FilterOn = false;
            keyField.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            keyField.BindData(data);
        }
    }
}