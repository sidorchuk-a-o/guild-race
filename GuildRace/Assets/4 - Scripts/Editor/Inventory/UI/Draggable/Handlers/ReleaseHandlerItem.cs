using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ReleaseHandler"/>
    /// </summary>
    public class ReleaseHandlerItem : ListItemElement
    {
        private LabelElement titleLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            titleLabel = root.CreateElement<LabelElement>(
                classNames: ClassNames.stretchCell);

            titleLabel.FontStyle(FontStyle.Bold);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            titleLabel.text = data.DataType.Name.SplitByUpper();
        }
    }
}