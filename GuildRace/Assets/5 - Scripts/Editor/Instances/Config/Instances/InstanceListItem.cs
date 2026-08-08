using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class InstanceListItem : ListItemElement<InstanceData>
    {
        private LabelElement idLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<LabelElement>(ClassNames.StretchCell);
            idLabel.TextElement.TextAnchor(TextAnchor.MiddleRight);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var id = data.GetProperty("id").GetValue();
            var type = data.GetProperty("type").GetValue<InstanceType>();
            var typeName = InstancesEditorState.Config.GetInstanceType(type).Title;

            idLabel.Text = $"{typeName} {id}";
        }
    }
}