using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class EquipGroupItem : ListItemElement<EquipGroupData>
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

            FoldoutOn = true;
            TitleLabel.EditButtonOn = true;

            idLabel.BindData(data);
        }
    }
}