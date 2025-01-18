using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipItemData))]
    public class EquipItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "equips";
        public override string SheetRange => "A:T";

        public override string NameLocalizeKey => IdKey;
        public override string SlotKey => "eq_slot_id";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var level = row["Level"].IntParse();
            var rarity = new Rarity(row["rarity_id"].IntParse());
            var type = new EquipType(row["eq_type_id"].IntParse());

            data.GetProperty("level").SetValue(level);
            data.GetProperty("rarity").SetValue(rarity);
            data.GetProperty("type").SetValue(type);

            var power = row["AP"].IntParse();
            var health = row["HP"].IntParse();
            var resource = row["Resource"].IntParse();
            var characterParams = data.GetProperty("characterParams");

            characterParams.GetProperty("power").SetValue(power);
            characterParams.GetProperty("health").SetValue(health);
            characterParams.GetProperty("resource").SetValue(resource);
        }
    }
}