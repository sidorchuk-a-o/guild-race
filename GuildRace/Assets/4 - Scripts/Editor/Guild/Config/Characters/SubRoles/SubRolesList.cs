using AD.ToolsCollection;

namespace Game.Guild
{
    public class SubRolesList : ListElement<SubRoleData, SubRoleItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(SubRoleCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}