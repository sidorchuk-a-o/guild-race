using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="SquadData"/>
    /// </summary>
    public class SquadItem : ListItemElement
    {
        private LabelElement titleLabel;

        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            titleLabel = root.CreateElement<LabelElement>(
                classNames: ClassNames.stretchCell);

            titleLabel
                .FontStyle(UnityEngine.FontStyle.Bold)
                .FontSize(14)
                .PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var type = data.GetProperty("instanceType").GetValue<InstanceType>();
            var typeName = InstancesEditorState.Config.GetInstanceType(type).Title;

            titleLabel.text = $"{typeName} Squad";
        }
    }
}