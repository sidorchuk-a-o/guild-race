using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Craft
{
    [CreateWizard(typeof(ReagentItemData))]
    public class ReagentItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "craft-data";
        public override string SheetRange => "H2:O";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var stack = new ItemStack(row["Stack Amount"].IntParse());
            var rarity = new Rarity(row["Rarity ID"].IntParse());

            data.GetProperty("stack").SetValue(stack);
            data.GetProperty("rarity").SetValue(rarity);
        }
    }
}