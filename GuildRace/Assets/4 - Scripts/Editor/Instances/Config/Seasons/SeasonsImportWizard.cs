using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Guild;

namespace Game.Instances
{
    [CreateWizard(typeof(SeasonData))]
    public class SeasonsImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter instancesImporter;
        private GoogleSheetsImporter boosUnitsImporter;
        private GoogleSheetsImporter abilitiesImporter;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-data";
        public override string SheetRange => "F2:H";

        protected override async void SaveCallback()
        {
            instancesImporter ??= new(SheetId, "boss-data", "N2:V", typeof(InstanceData));
            boosUnitsImporter ??= new(SheetId, "boss-units", "A:U", typeof(UnitData));
            abilitiesImporter ??= new(SheetId, "unit-abilities", "A:Q", typeof(AbilityData));

            await instancesImporter.LoadData("ID");
            await boosUnitsImporter.LoadData("ID");
            await abilitiesImporter.LoadData("ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData seasonData, IDataRow row)
        {
            base.UpdateData(seasonData, row);

            // name key
            var nameKey = row["Name Key"].LocalizeKeyParse();

            seasonData.GetProperty("nameKey").SetValue(nameKey);

            // instances
            ImportInstances(seasonData);
        }

        private void ImportInstances(SerializedData seasonData)
        {
            var id = seasonData.GetProperty("id").GetValue<int>();

            var instancesData = seasonData.GetProperty("instances");
            var instancesSaveMeta = new SaveMeta(isSubObject: true, instancesData);

            instancesImporter.ImportData(instancesSaveMeta, CheckInstanceEqual, UpdateInstanceData, onFilterRow: row =>
            {
                var seasonId = row["Season ID"].IntParse();

                return id == seasonId;
            });
        }

        private bool CheckInstanceEqual(SerializedData instanceData, IDataRow row)
        {
            var dataId = instanceData.GetProperty("id").GetValue<int>();
            var rowId = row["ID"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateInstanceData(SerializedData instanceData, IDataRow row)
        {
            var id = row["ID"].IntParse();
            var title = row["Name"].ToUpperFirst();
            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var type = new InstanceType(row["Type ID"].IntParse());

            instanceData.GetProperty("id").SetValue(id);
            instanceData.GetProperty("title").SetValue(title);
            instanceData.GetProperty("nameKey").SetValue(nameKey);
            instanceData.GetProperty("descKey").SetValue(descKey);
            instanceData.GetProperty("type").SetValue(type);

            ImportBossUnits(instanceData);
        }

        private void ImportBossUnits(SerializedData instanceData)
        {
            var id = instanceData.GetProperty("id").GetValue<int>();

            var unitsData = instanceData.GetProperty("boosUnits");
            var unitsSaveMeta = new SaveMeta(isSubObject: true, unitsData);

            boosUnitsImporter.ImportData(unitsSaveMeta, CheckBossUnitEqual, UpdateBoosUnitData, onFilterRow: row =>
            {
                var instanceId = row["Instance ID"].IntParse();

                return id == instanceId;
            });
        }

        private bool CheckBossUnitEqual(SerializedData unitData, IDataRow row)
        {
            var dataId = unitData.GetProperty("id").GetValue<int>();
            var rowId = row["ID"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateBoosUnitData(SerializedData unitData, IDataRow row)
        {
            var id = row["ID"].IntParse();
            var title = row["Name"].ToUpperFirst();
            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var imageRef = row["Icon Name"].AddressableFileParse();

            unitData.GetProperty("id").SetValue(id);
            unitData.GetProperty("title").SetValue(title);
            unitData.GetProperty("nameKey").SetValue(nameKey);
            unitData.GetProperty("descKey").SetValue(descKey);
            unitData.GetProperty("imageRef").SetValue(imageRef);

            ImportAbilities(unitData);

            ClassImportWizard.ImportUnitParams(unitData, row);
        }

        private void ImportAbilities(SerializedData unitData)
        {
            var id = unitData.GetProperty("id").GetValue<int>();

            var abilitiesData = unitData.GetProperty("abilities");
            var abilitiesSaveMeta = new SaveMeta(isSubObject: true, abilitiesData);

            abilitiesImporter.ImportData(abilitiesSaveMeta, CheckAbilityEqual, UpdateAbilityData, onFilterRow: row =>
            {
                var unitId = row["Unit ID"].IntParse();

                return id == unitId;
            });
        }

        private bool CheckAbilityEqual(SerializedData abilityData, IDataRow row)
        {
            var dataId = abilityData.GetProperty("id").GetValue<int>();
            var rowId = row["ID"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateAbilityData(SerializedData abilityData, IDataRow row)
        {
            var id = row["ID"].IntParse();
            var title = row["Name"].ToUpperFirst();
            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            abilityData.GetProperty("id").SetValue(id);
            abilityData.GetProperty("title").SetValue(title);
            abilityData.GetProperty("nameKey").SetValue(nameKey);
            abilityData.GetProperty("descKey").SetValue(descKey);
            abilityData.GetProperty("iconRef").SetValue(iconRef);
        }
    }
}