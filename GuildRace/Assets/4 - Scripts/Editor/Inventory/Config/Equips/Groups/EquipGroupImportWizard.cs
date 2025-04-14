using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipGroupData))]
    public class EquipGroupImportWizard : EntitiesImportWizard<int>
    {
        private GoogleSheetsImporter equipTypesImporter;

        public override string IdKey => "ID";
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

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            // name key
            var nameKey = row["Name Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(nameKey);

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
                var groupId = row["Group ID"].IntParse();

                return id == groupId;
            });
        }

        private void UpdateTypeData(SerializedData data, IDataRow row)
        {
            var id = row[IdKey].IntParse();
            var title = row[TitleKey].ToUpperFirst();
            var nameKey = row["Name Key"].LocalizeKeyParse();

            data.GetProperty("id").SetValue(id);
            data.GetProperty("title").SetValue(title);
            data.GetProperty("nameKey").SetValue(nameKey);
        }
    }
}