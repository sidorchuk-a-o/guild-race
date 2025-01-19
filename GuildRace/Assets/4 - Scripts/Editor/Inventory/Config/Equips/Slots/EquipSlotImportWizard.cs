using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(EquipSlotData))]
    public class EquipSlotImportWizard : ItemSlotImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "inventory-data";
        public override string SheetRange => "E2:I";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var equipGroup = new EquipGroup(row["Group Id"].IntParse());

            data.GetProperty("equipGroup").SetValue(equipGroup);
        }
    }
}