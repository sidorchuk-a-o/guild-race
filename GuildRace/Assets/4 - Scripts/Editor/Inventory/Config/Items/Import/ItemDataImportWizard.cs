using System.Linq;
using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    /// <summary>
    /// Import: <see cref="ItemData"/>
    /// </summary>
    public abstract class ItemDataImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public virtual string NameLocalizeKey => "Name Key";
        public virtual string SlotKey => "Slot ID";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var slot = new ItemSlot(row[SlotKey].IntParse());
            var nameKey = row[NameLocalizeKey].LocalizeKeyParse();
            var iconRef = row["Icon Name"].AddressableFileParse();

            data.GetProperty("slot").SetValue(slot);
            data.GetProperty("iconRef").SetValue(iconRef);
            data.GetProperty("nameKey").SetValue(nameKey);

            var dataType = data.DataType;
            var itemsParams = InventoryEditorState.Config.ItemsParams;
            var dataFactory = itemsParams.Factories.FirstOrDefault(x => x.DataType == dataType);

            data.GetProperty("itemType").SetValue(dataFactory.ItemType);
        }
    }
}