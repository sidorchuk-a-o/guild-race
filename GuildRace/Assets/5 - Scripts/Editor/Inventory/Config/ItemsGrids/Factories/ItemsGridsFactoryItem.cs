using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class ItemsGridsFactoryItem : ListItemElement<ItemsGridsFactory>
    {
        private LabelElement typeNameLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            typeNameLabel = root.CreateElement<LabelElement>(ClassNames.StretchCell);

            typeNameLabel
                .FontStyle(FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            typeNameLabel.Text = data.DataType.Name
                .Clear("Factory")
                .SplitByUpper();
        }
    }
}