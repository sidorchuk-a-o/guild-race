using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="OptionHandler"/>
    /// </summary>
    public class OptionHandlerItem : ListItemElement
    {
        private Label titleLabel;

        protected override IEditorsFactory EditorsFactory => InventoryEditorState.OptionHandlerFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            titleLabel = root.Create<Label>(classNames: ClassNames.stretchCell);
            titleLabel
                .FontStyle(FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.text = EditorsFactory.GetEditorData(data.DataType).MenuKey;
        }
    }
}