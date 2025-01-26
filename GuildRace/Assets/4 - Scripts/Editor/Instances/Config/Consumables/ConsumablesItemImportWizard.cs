using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Instances
{
    [CreateWizard(typeof(ConsumablesItemData))]
    public class ConsumablesItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "consumables";
        public override string SheetRange => "A:J";

        public override string TitleKey => "Title";
        public override string NameLocalizeKey => "localization title key";
        public override string SlotKey => string.Empty;

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var stack = new ItemStack(100);
            var rarity = new Rarity(row["Rarity ID"].IntParse());
            var descKey = row["localization description key"].LocalizeKeyParse();

            data.GetProperty("stack").SetValue(stack);
            data.GetProperty("rarity").SetValue(rarity);
            data.GetProperty("descKey").SetValue(descKey);
        }
    }
}