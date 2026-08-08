using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class SquadItem : ListItemElement<SquadData>
    {
        private LabelElement titleLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            titleLabel = root.CreateElement<LabelElement>(ClassNames.StretchCell);

            titleLabel
                .FontStyle(FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var type = data.GetProperty("instanceType").GetValue<InstanceType>();
            var typeName = InstancesEditorState.Config.GetInstanceType(type).Title;

            titleLabel.Text = $"{typeName} Squad";
        }
    }
}