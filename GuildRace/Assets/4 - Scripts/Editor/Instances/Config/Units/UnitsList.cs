using AD.ToolsCollection;

namespace Game.Instances
{
    public class UnitsList : ListElement<UnitData, UnitItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Units";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}