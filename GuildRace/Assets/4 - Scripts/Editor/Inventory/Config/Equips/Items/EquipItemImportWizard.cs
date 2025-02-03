﻿using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipItemData))]
    public class EquipItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "equips";
        public override string SheetRange => "A:U";

        public override string TitleKey => IdKey;
        public override string NameLocalizeKey => IdKey;

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var level = row["Level"].IntParse();
            var rarity = new Rarity(row["Rarity ID"].IntParse());
            var type = new EquipType(row["Type ID"].IntParse());

            data.GetProperty("level").SetValue(level);
            data.GetProperty("rarity").SetValue(rarity);
            data.GetProperty("type").SetValue(type);

            var power = row["AP"].FloatParse();
            var armor = row["Armor"].FloatParse();
            var health = row["HP"].FloatParse();
            var resource = row["Resource"].FloatParse();
            var characterParams = data.GetProperty("characterParams");

            characterParams.GetProperty("power").SetValue(power);
            characterParams.GetProperty("armor").SetValue(armor);
            characterParams.GetProperty("health").SetValue(health);
            characterParams.GetProperty("resource").SetValue(resource);
        }
    }
}