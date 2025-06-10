using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    public class ItemsFactoryItem : EntityListItemElement
    {
        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.editButtonOn = true;
        }
    }
}