using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Import: <see cref="ItemData"/>
    /// </summary>
    public abstract class ItemDataImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "Id";
        public override string TitleKey => "Id";

        public abstract string NameLocalizeKey { get; }
        public abstract string SlotKey { get; }

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var slot = new ItemSlot(row[SlotKey].IntParse());
            var localizeKey = row[NameLocalizeKey].LocalizeKeyParse();
            var iconRef = row["icon_name"].AddressableFileParse();

            data.GetProperty("slot").SetValue(slot);
            data.GetProperty("iconRef").SetValue(iconRef);
            data.GetProperty("nameKey").SetValue(localizeKey);
        }
    }
}