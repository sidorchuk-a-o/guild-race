using AD.ToolsCollection;

namespace Game.Inventory
{
    public class EquipGroupsList : ListElement<EquipGroupData, EquipGroupItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Groups";
            wizardType = typeof(EquipGroupImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}