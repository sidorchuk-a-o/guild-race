using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Craft
{
    [CreateWizard(typeof(ReagentItemData))]
    public class ReagentItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "craft-data";
        public override string SheetRange => "E2:G";

        public override string TitleKey => "Name";
        public override string NameLocalizeKey => "Localize Key";
        public override string SlotKey => string.Empty;

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            data.GetProperty("stack").SetValue(new ItemStack(200));
        }
    }
}