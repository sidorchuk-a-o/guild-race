using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class ItemsGridCellTypeItem : ListItemElement<ItemsGridCellTypeData>
    {
        private ItemIdElement idLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<ItemIdElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindData(data);

            TitleLabel.EditButtonOn = true;
        }
    }
}