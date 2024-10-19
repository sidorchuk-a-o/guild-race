using AD.ToolsCollection;

namespace Game.Items
{
    public class EquipGroupsList : ListElement<EquipGroupData, EquipGroupItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Groups";
            wizardType = typeof(EquipGroupCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}