using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;

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

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            var localizeKey = row[NameLocalizeKey].LocalizeKeyParse();
            var slot = new ItemSlot(row["eq_slot_id"].IntParse());
            var source = new ItemSource(row["source_id"].IntParse());
            var iconRef = row["icon_name"].AddressableFileParse();

            data.GetProperty("iconRef").SetValue(iconRef);
            data.GetProperty("nameKey").SetValue(localizeKey);
            data.GetProperty("slot").SetValue(slot);
            data.GetProperty("source").SetValue(source);
        }
    }
}