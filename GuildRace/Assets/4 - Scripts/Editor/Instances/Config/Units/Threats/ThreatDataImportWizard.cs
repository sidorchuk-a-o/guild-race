using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(ThreatData))]
    public class ThreatDataImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "threats";
        public override string SheetRange => "A2:G";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            data.GetProperty("nameKey").SetValue(nameKey);
            data.GetProperty("descKey").SetValue(descKey);
            data.GetProperty("iconRef").SetValue(iconRef);
        }
    }
}