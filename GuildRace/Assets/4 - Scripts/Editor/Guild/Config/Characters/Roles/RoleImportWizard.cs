using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Guild
{
    [CreateWizard(typeof(RoleData))]
    public class RoleImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "class-data";
        public override string SheetRange => "A2:E";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            data.GetProperty("nameKey").SetValue(nameKey);
            data.GetProperty("iconRef").SetValue(iconRef);
        }
    }
}