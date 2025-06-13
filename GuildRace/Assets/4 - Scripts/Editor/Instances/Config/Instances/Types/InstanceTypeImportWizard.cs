using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(InstanceTypeData))]
    public class InstanceTypeImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-data";
        public override string SheetRange => "K2:M";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(nameKey);
        }
    }
}