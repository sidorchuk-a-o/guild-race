using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Import: <see cref="ItemSlotData"/>
    /// </summary>
    public abstract class ItemSlotImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();

            data.GetProperty("nameKey").SetValue(nameKey);
        }
    }
}