using AD.ToolsCollection;

namespace Game.Instances
{
    public class AbilitiesList : ListElement<AbilityData, AbilityItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Abilities";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}