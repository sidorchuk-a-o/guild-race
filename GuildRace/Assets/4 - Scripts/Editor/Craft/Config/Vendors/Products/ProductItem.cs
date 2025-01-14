using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="ProductData"/>
    /// </summary>
    public class ProductItem : EntityListItemElement
    {
        private PropertyElement countField;
        private PopupElement<int> itemPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();

            countField = root.CreateProperty();
            countField.Width(100);
            countField.labelOn = false;

            itemPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);
            itemPopup.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            countField.BindProperty("count", data);
            itemPopup.BindProperty("itemId", data);
        }
    }
}