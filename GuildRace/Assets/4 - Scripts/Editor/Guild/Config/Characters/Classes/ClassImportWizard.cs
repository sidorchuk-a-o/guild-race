﻿using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;

namespace Game.Guild
{
    [CreateWizard(typeof(ClassData))]
    public class ClassImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter specsImporter;

        public override string IdKey => "Class ID";
        public override string TitleKey => "Class";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "classes";
        public override string SheetRange => "A:AB";

        protected override async void SaveCallback()
        {
            specsImporter ??= new(SheetId, SheetName, SheetRange, typeof(SpecializationData));

            await specsImporter.LoadData("Spec ID");

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Class Name Loc Key"].LocalizeKeyParse();
            var descKey = row["Class Desc Loc Key"].LocalizeKeyParse();
            var armorType = new EquipType(row["Armortype ID"].IntParse());
            var weaponType = new EquipType(row["Weapontype ID"].IntParse());

            data.GetProperty("nameKey").SetValue(nameKey);
            data.GetProperty("descKey").SetValue(descKey);
            data.GetProperty("armorType").SetValue(armorType);
            data.GetProperty("weaponType").SetValue(weaponType);

            // specs
            var id = data.GetProperty("id").GetValue<int>();

            var specsData = data.GetProperty("specs");
            var specsSaveMeta = new SaveMeta(isSubObject: true, specsData);

            specsImporter.ImportData(specsSaveMeta, CheckSpecEqual, UpdateSpecData, onFilterRow: row =>
            {
                var classId = row["Class ID*"].IntParse();

                return id == classId;
            });
        }

        private bool CheckSpecEqual(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            var dataId = data.GetProperty("id").GetValue<int>();
            var rowId = row["Spec ID"].IntParse();

            return dataId == rowId;
        }

        private void UpdateSpecData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            var id = row["Spec ID"].IntParse();
            var title = row["Spec"].ToUpperFirst();
            var nameKey = row["Spec Name Loc Key"].LocalizeKeyParse();
            var descKey = row["Spec Desc Loc Key"].LocalizeKeyParse();
            var roleId = new RoleId(row["Role ID"].IntParse());
            var subRoleId = new SubRoleId(row["Subrole ID"].IntParse());

            data.GetProperty("id").SetValue(id);
            data.GetProperty("title").SetValue(title);

            data.GetProperty("nameKey").SetValue(nameKey);
            data.GetProperty("descKey").SetValue(descKey);
            data.GetProperty("roleId").SetValue(roleId);
            data.GetProperty("subRoleId").SetValue(subRoleId);
        }
    }
}