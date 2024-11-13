using AD.ToolsCollection;

namespace Game.Inventory
{
    public class EquipTypesList : ListElement<EquipTypeData, EquipTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Equip Types";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}