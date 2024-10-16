using AD.ToolsCollection;

namespace Game.Guild
{
    public class RolesList : ListElement<RoleData, RoleItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(RoleCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}