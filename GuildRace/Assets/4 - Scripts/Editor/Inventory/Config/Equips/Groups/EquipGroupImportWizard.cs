using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipGroupData))]
    public class EquipGroupImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter equipTypesImporter;

        public override string IdKey => "Id";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "inventory-data";
        public override string SheetRange => "A2:C";

        protected override async void SaveCallback()
        {
            equipTypesImporter ??= new(SheetId, SheetName, "K2:O", typeof(EquipTypeData));

            await equipTypesImporter.LoadData(IdKey);

            base.SaveCallback();
        }

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            // name key
            var localizeKey = row["Localize Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(localizeKey);

            // types
            ImportEquipTypes(data);
        }

        private void ImportEquipTypes(SerializedData data)
        {
            var id = data.GetProperty("id").GetValue<int>();

            var typesData = data.GetProperty("types");
            var typesSaveMeta = new SaveMeta(isSubObject: true, typesData);

            equipTypesImporter.ImportData(typesSaveMeta, CheckEqual, UpdateTypeData, onFilterRow: row =>
            {
                var groupId = row["Group Id"].IntParse();

                return id == groupId;
            });
        }

        private void UpdateTypeData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            var id = row[IdKey].IntParse();
            var title = row[TitleKey].ToUpperFirst();
            var localizeKey = row["Localize Key"].LocalizeKeyParse();

            data.GetProperty("id").SetValue(id);
            data.GetProperty("title").SetValue(title);
            data.GetProperty("nameKey").SetValue(localizeKey);
        }
    }
}