using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    public class DefaultCharacterItem : ListItemElement<DefaultCharacterData>
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

            titleLabel.Text = GetTitle(data);
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