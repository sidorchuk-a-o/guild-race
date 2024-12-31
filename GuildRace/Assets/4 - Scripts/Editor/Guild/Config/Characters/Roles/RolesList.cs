using AD.ToolsCollection;

namespace Game.Guild
{
    public class RolesList : ListElement<RoleData, RoleItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(RoleImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}