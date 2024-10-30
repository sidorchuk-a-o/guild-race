using AD.ToolsCollection;

namespace Game.Inventory
{
    public class EquipTypesList : ListElement<EquipTypeData, EquipTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Types";
            wizardType = typeof(EquipTypeCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}