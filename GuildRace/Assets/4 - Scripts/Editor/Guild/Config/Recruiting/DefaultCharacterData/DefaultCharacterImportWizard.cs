using System.Linq;
using AD.ToolsCollection;

namespace Game.Guild
{
    [CreateWizard(typeof(DefaultCharacterData))]
    public class DefaultCharacterImportWizard : GoogleSheetsImportWizard
    {
        public override string ValidKey => "Spec ID";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "class-data";
        public override string SheetRange => "N2:P";

        protected override bool CheckEqual(SerializedData data, IDataRow row)
        {
            return false;
        }

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            var specId = row["Spec ID"].IntParse();

            var classes = GuildEditorState.Config.CharactersParams.Classes;
            var classId = classes.FirstOrDefault(c => c.Specs.Any(s => s.Id == specId)).Id;

            data.GetProperty("classId").SetValue((ClassId)classId);
            data.GetProperty("specId").SetValue((SpecializationId)specId);
        }
    }
}