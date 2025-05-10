using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Instances;
using Game.Inventory;

namespace Game.Guild
{
    [CreateWizard(typeof(ClassData))]
    public class ClassImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter specsImporter;
        private GoogleSheetsImporter abilitiesImporter;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "classes";
        public override string SheetRange => "A:Z";

        protected override async void SaveCallback()
        {
            LockWizard();

            specsImporter ??= new(SheetId, SheetName, SheetRange, typeof(SpecializationData));
            abilitiesImporter ??= new(SheetId, "class-abilities", "A:I", typeof(AbilityData));

            await specsImporter.LoadData("Spec ID");
            await abilitiesImporter.LoadData("ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData classData, IDataRow row)
        {
            base.UpdateData(classData, row);

            var nameKey = row["Class Name Key"].LocalizeKeyParse();
            var descKey = row["Class Desc Key"].LocalizeKeyParse();
            var iconRef = row["Class Icon Name"].AddressableFileParse();
            var armorType = new EquipType(row["Armor ID"].IntParse());
            var weaponType = new EquipType(row["Weapon ID"].IntParse());

            classData.GetProperty("nameKey").SetValue(nameKey);
            classData.GetProperty("descKey").SetValue(descKey);
            classData.GetProperty("iconRef").SetValue(iconRef);
            classData.GetProperty("armorType").SetValue(armorType);
            classData.GetProperty("weaponType").SetValue(weaponType);

            ImportSpecs(classData);
        }

        private void ImportSpecs(SerializedData data)
        {
            var id = data.GetProperty("id").GetValue<int>();

            var specsData = data.GetProperty("specs");
            var specsSaveMeta = new SaveMeta(isSubObject: true, specsData);

            specsImporter.ImportData(specsSaveMeta, CheckSpecEqual, UpdateSpecData, onFilterRow: row =>
            {
                var classId = row["Class ID"].IntParse();

                return id == classId;
            });
        }

        private bool CheckSpecEqual(SerializedData data, IDataRow row)
        {
            var dataId = data.GetProperty("id").GetValue<int>();
            var rowId = row["Spec ID"].IntParse();

            return dataId == rowId;
        }

        private void UpdateSpecData(SerializedData specData, IDataRow row)
        {
            var id = row["Spec ID"].IntParse();
            var title = row["Spec"].ToUpperFirst();
            var nameKey = row["Spec Name Key"].LocalizeKeyParse();
            var descKey = row["Spec Desc Key"].LocalizeKeyParse();
            var iconRef = row["Spec Icon Name"].AddressableFileParse();
            var roleId = new RoleId(row["Role ID"].IntParse());
            var subRoleId = new SubRoleId(row["Subrole ID"].IntParse());

            specData.GetProperty("id").SetValue(id);
            specData.GetProperty("title").SetValue(title);

            specData.GetProperty("nameKey").SetValue(nameKey);
            specData.GetProperty("descKey").SetValue(descKey);
            specData.GetProperty("iconRef").SetValue(iconRef);
            specData.GetProperty("roleId").SetValue(roleId);
            specData.GetProperty("subRoleId").SetValue(subRoleId);

            // params
            ImportUnitParams(specData, row);
            ImportAbilities(specData, abilitiesImporter);
        }

        public static void ImportUnitParams(SerializedData specData, IDataRow row)
        {
            var health = row["HP"].FloatParse();
            var power = row["AP"].FloatParse();

            var unitParams = specData.GetProperty("unitParams");
            unitParams.GetProperty("health").SetValue(health);
            unitParams.GetProperty("power").SetValue(power);
        }

        public static void ImportAbilities(SerializedData unitData, GoogleSheetsImporter abilitiesImporter)
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

        private static bool CheckAbilityEqual(SerializedData abilityData, IDataRow row)
        {
            var dataId = abilityData.GetProperty("id").GetValue<int>();
            var rowId = row["ID"].IntParse();

            return Equals(dataId, rowId);
        }

        private static void UpdateAbilityData(SerializedData abilityData, IDataRow row)
        {
            var id = row["ID"].IntParse();
            var title = row["Name"].ToUpperFirst();
            var nameKey = row["Name Key"].LocalizeKeyParse();
            var descKey = row["Desc Key"].LocalizeKeyParse();
            var threatId = new ThreatId(row["Threat ID"].IntParse());

            abilityData.GetProperty("id").SetValue(id);
            abilityData.GetProperty("title").SetValue(title);
            abilityData.GetProperty("nameKey").SetValue(nameKey);
            abilityData.GetProperty("descKey").SetValue(descKey);
            abilityData.GetProperty("threatId").SetValue(threatId);
        }
    }
}