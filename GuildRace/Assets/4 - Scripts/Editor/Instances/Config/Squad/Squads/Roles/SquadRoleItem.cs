using AD.ToolsCollection;
using Game.Guild;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="SquadRoleData"/>
    /// </summary>
    public class SquadRoleItem : ListItemElement
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
            foldoutOn = true;

            base.BindData(data);

            var type = data.GetProperty("role").GetValue<RoleId>();
            var typeName = GuildEditorState.Config.CharactersParams.GetRole(type).Title;

            titleLabel.text = $"{typeName} Role";
        }
    }
}