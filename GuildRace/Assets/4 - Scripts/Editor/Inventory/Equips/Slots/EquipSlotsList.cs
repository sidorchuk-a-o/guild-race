using AD.ToolsCollection;

namespace Game.Inventory
{
    public class EquipSlotsList : ItemSlotsList<EquipSlotData>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Slots";
            wizardType = typeof(EquipSlotImportWizard);

            base.BindData(data);
        }
    }
}