using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(RarityData))]
    public class RarityImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "inventory-data";
        public override string SheetRange => "Q2:S";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(nameKey);
        }
    }
}