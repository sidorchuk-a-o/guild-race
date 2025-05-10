using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    public class ItemsGridsFactoryItem : ListItemElement
    {
        private LabelElement typeNameLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            typeNameLabel = root.CreateElement<LabelElement>(
                classNames: ClassNames.stretchCell);

            typeNameLabel
                .FontStyle(UnityEngine.FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            typeNameLabel.text = data.DataType.Name
                .Clear("Factory")
                .SplitByUpper();
        }
    }
}