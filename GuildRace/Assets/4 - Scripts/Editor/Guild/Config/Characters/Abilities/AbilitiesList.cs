using AD.ToolsCollection;

namespace Game.Guild
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