using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Item: <see cref="DefaultCharacterData"/>
    /// </summary>
    public class DefaultCharacterItem : ListItemElement
    {
        protected override IEditorsFactory EditorsFactory => GuildEditorState.EditorsFactory;

        private LabelElement titleLabel;

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

            titleLabel.text = GetTitle(data);
        }

        public static string GetTitle(SerializedData data)
        {
            var charactersParams = GuildEditorState.Config.CharactersParams;

            var classId = data.GetProperty("classId").GetValue<ClassId>();
            var classData = charactersParams.GetClass(classId);

            var specId = data.GetProperty("specId").GetValue<SpecializationId>();
            var specData = charactersParams.GetSpecialization(specId);

            var roleId = specData.RoleId;
            var roleData = charactersParams.GetRole(roleId);

            return $"{roleData.Title} - {classData.Title} - {specData.Title}";
        }
    }
}