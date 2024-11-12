using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipItemData))]
    public class EquipItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "equips";
        public override string SheetRange => "A:R";

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);

            var level = 10;
            var power = 10;
            var rarity = new Rarity();
            var type = new EquipType(row["eq_type_id"].IntParse());

            data.GetProperty("level").SetValue(level);
            data.GetProperty("power").SetValue(power);
            data.GetProperty("rarity").SetValue(rarity);
            data.GetProperty("type").SetValue(type);
        }
    }
}