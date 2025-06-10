using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemGridUIParams"/>
    /// </summary>
    public class ItemGridUIParamsItem : ListItemElement
    {
        private KeyElement<int> itemTypeField;

        protected override IEditorsFactory EditorsFactory => InventoryEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            itemTypeField = root.CreateKey<ItemType, int>();
            itemTypeField.FlexGrow(1).ReadOnly();
            itemTypeField.filterOn = false;
            itemTypeField.removeOn = false;
            itemTypeField.updateOn = false;
            itemTypeField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            foldoutOn = true;

            base.BindData(data);

            itemTypeField.BindProperty("itemType", data);
        }
    }
}