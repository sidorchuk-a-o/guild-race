using System.Linq;
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
        public override string SheetRange => "A:Q";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var descKey = row["Desc Key"].LocalizeKeyParse();
            var stack = new ItemStack(row["Stack Amount"].IntParse());
            var rarity = new Rarity(row["Rarity ID"].IntParse());
            var type = new ConsumableType(row["Type ID"].IntParse());
            var mechanicId = row["Mechanic ID"].IntParse();

            var mechanicParamsData = new[] { row["Param 1"] };
            var mechanicParams = mechanicParamsData
                .Where(x => x.IsValid())
                .ToList();

            data.GetProperty("descKey").SetValue(descKey);
            data.GetProperty("stack").SetValue(stack);
            data.GetProperty("rarity").SetValue(rarity);
            data.GetProperty("type").SetValue(type);
            data.GetProperty("mechanicId").SetValue(mechanicId);
            data.GetProperty("mechanicParams").SetValue(mechanicParams);
        }
    }
}