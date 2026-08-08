using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class ReleaseHandlerItem : ListItemElement<ReleaseHandler>
    {
        private LabelElement titleLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            titleLabel = root.CreateElement<LabelElement>(ClassNames.StretchCell);
            titleLabel.FontStyle(FontStyle.Bold);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.Text = data.DataType.Name.SplitByUpper();
        }
    }
}