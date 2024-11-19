using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Instances
{
    [CreateWizard(typeof(SeasonData))]
    public class SeasonsImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter instancesImporter;

        public override string IdKey => "Id";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-data";
        public override string SheetRange => "F2:H";

        protected override async void SaveCallback()
        {
            instancesImporter ??= new(SheetId, SheetName, "N2:V", typeof(InstanceData));

            await instancesImporter.LoadData();

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            // name key
            var localizeKey = row["Localize Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(localizeKey);

            // instances
            var id = data.GetProperty("id").GetValue<int>();

            var instancesData = data.GetProperty("instances");
            var instancesSaveMeta = new SaveMeta(isSubObject: true, instancesData);

            instancesImporter.ImportData(instancesSaveMeta, CheckEqual, UpdateInstanceData, onFilterRow: row =>
            {
                var seasonId = row["Season Id"].IntParse();

                return id == seasonId;
            });
        }

        private void UpdateInstanceData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            var id = row[IdKey].IntParse();
            var title = row[TitleKey].ToUpperFirst();
            var localizeKey = row["Localize Key"].LocalizeKeyParse();
            var descLocalizeKey = row["Desc Localize Key"].LocalizeKeyParse();
            var type = new InstanceType(row["Instance Type Id"].IntParse());

            data.GetProperty("id").SetValue(id);
            data.GetProperty("title").SetValue(title);
            data.GetProperty("nameKey").SetValue(localizeKey);
            data.GetProperty("descKey").SetValue(descLocalizeKey);
            data.GetProperty("type").SetValue(type);
        }
    }
}