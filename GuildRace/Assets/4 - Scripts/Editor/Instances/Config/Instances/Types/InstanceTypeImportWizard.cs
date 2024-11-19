using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Instances
{
    [CreateWizard(typeof(InstanceTypeData))]
    public class InstanceTypeImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "Id";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-data";
        public override string SheetRange => "J2:L";

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            var localizeKey = row["Localize Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(localizeKey);
        }
    }
}