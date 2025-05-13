using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    public class ItemIdItem : ListItemElement
    {
        private PopupElement<int> itemPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            itemPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);
            itemPopup.FlexGrow(1);
            itemPopup.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            itemPopup.BindData(data);
        }
    }
}