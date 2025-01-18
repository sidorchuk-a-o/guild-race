using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(SeasonData))]
    public class SeasonsImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter instancesImporter;
        private GoogleSheetsImporter boosUnitsImporter;
        private GoogleSheetsImporter abilitiesImporter;

        public override string IdKey => "Id";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-data";
        public override string SheetRange => "F2:H";

        protected override async void SaveCallback()
        {
            instancesImporter ??= new(SheetId, "boss-data", "N2:V", typeof(InstanceData));
            boosUnitsImporter ??= new(SheetId, "boss-units", "A:Q", typeof(UnitData));
            abilitiesImporter ??= new(SheetId, "unit-abilities", "A:M", typeof(AbilityData));

            await instancesImporter.LoadData("Id");
            await boosUnitsImporter.LoadData("Id");
            await abilitiesImporter.LoadData("Ability ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData seasonData, IDataRow row)
        {
            base.UpdateData(seasonData, row);

            // name key
            var localizeKey = row["Localize Key"].LocalizeKeyParse();

            seasonData.GetProperty("nameKey").SetValue(localizeKey);

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
                var seasonId = row["Season Id"].IntParse();

                return id == seasonId;
            });
        }

        private bool CheckInstanceEqual(SerializedData instanceData, IDataRow row)
        {
            var dataId = instanceData.GetProperty("id").GetValue<int>();
            var rowId = row["Id"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateInstanceData(SerializedData instanceData, IDataRow row)
        {
            var id = row["Id"].IntParse();
            var title = row["Name"].ToUpperFirst();
            var nameKey = row["Localize Key"].LocalizeKeyParse();
            var descKey = row["Desc Localize Key"].LocalizeKeyParse();
            var type = new InstanceType(row["Instance Type Id"].IntParse());

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
            var rowId = row["Id"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateBoosUnitData(SerializedData unitData, IDataRow row)
        {
            var id = row["Id"].IntParse();
            var title = row["Unit Name"].ToUpperFirst();
            var nameKey = row["Name Localize Key"].LocalizeKeyParse();
            var descKey = row["Desc Localize Key"].LocalizeKeyParse();
            var imageRef = row["Image Name"].AddressableFileParse();

            unitData.GetProperty("id").SetValue(id);
            unitData.GetProperty("title").SetValue(title);
            unitData.GetProperty("nameKey").SetValue(nameKey);
            unitData.GetProperty("descKey").SetValue(descKey);
            unitData.GetProperty("imageRef").SetValue(imageRef);

            ImportAbilities(unitData);
        }

        private void ImportAbilities(SerializedData unitData)
        {
            var id = unitData.GetProperty("id").GetValue<int>();

            var abilitiesData = unitData.GetProperty("abilities");
            var abilitiesSaveMeta = new SaveMeta(isSubObject: true, abilitiesData);

            abilitiesImporter.ImportData(abilitiesSaveMeta, CheckAbilityEqual, UpdateAbilityData, onFilterRow: row =>
            {
                var unitId = row["Boss ID"].IntParse();

                return id == unitId;
            });
        }

        private bool CheckAbilityEqual(SerializedData abilityData, IDataRow row)
        {
            var dataId = abilityData.GetProperty("id").GetValue<int>();
            var rowId = row["Ability ID"].IntParse();

            return Equals(dataId, rowId);
        }

        private void UpdateAbilityData(SerializedData abilityData, IDataRow row)
        {
            var id = row["Ability ID"].IntParse();
            var title = row["Ability Name"].ToUpperFirst();
            var nameKey = row["Localization Key (Title)"].LocalizeKeyParse();
            var descKey = row["Localization Key (Description)"].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            abilityData.GetProperty("id").SetValue(id);
            abilityData.GetProperty("title").SetValue(title);
            abilityData.GetProperty("nameKey").SetValue(nameKey);
            abilityData.GetProperty("descKey").SetValue(descKey);
            abilityData.GetProperty("iconRef").SetValue(iconRef);
        }
    }
}