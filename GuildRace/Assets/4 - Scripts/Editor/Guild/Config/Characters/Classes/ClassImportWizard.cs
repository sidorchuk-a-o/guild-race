using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Guild
{
    [CreateWizard(typeof(ClassData))]
    public class ClassImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter specsImporter;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "classes";
        public override string SheetRange => "A:AH";

        protected override async void SaveCallback()
        {
            specsImporter ??= new(SheetId, SheetName, SheetRange, typeof(SpecializationData));

            await specsImporter.LoadData("Spec ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData classData, IDataRow row)
        {
            base.UpdateData(classData, row);

            var nameKey = row["Class Name Key"].LocalizeKeyParse();
            var descKey = row["Class Desc Key"].LocalizeKeyParse();
            var armorType = new EquipType(row["Armor ID"].IntParse());
            var weaponType = new EquipType(row["Weapon ID"].IntParse());

            classData.GetProperty("nameKey").SetValue(nameKey);
            classData.GetProperty("descKey").SetValue(descKey);
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
            var roleId = new RoleId(row["Role ID"].IntParse());
            var subRoleId = new SubRoleId(row["Subrole ID"].IntParse());

            specData.GetProperty("id").SetValue(id);
            specData.GetProperty("title").SetValue(title);

            specData.GetProperty("nameKey").SetValue(nameKey);
            specData.GetProperty("descKey").SetValue(descKey);
            specData.GetProperty("roleId").SetValue(roleId);
            specData.GetProperty("subRoleId").SetValue(subRoleId);

            // unit params
            ImportUnitParams(specData, row);
        }

        public static void ImportUnitParams(SerializedData specData, IDataRow row)
        {
            var health = row["HP"].FloatParse();
            var power = row["AP"].FloatParse();
            var resourceValue = row["RES"].FloatParse();
            var resourceRegen = row["RES Regen"].FloatParse();

            var unitParams = specData.GetProperty("unitParams");
            unitParams.GetProperty("health").SetValue(health);
            unitParams.GetProperty("power").SetValue(power);

            var resourceParams = unitParams.GetProperty("resourceParams");
            resourceParams.GetProperty("maxValue").SetValue(resourceValue);
            resourceParams.GetProperty("regenValue").SetValue(resourceRegen);
        }
    }
}