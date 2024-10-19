using AD.ToolsCollection;

namespace Game.Items
{
    public class EquipSlotsList : ListElement<EquipSlotData, EquipSlotItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Slots";
            wizardType = typeof(EquipSlotCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}