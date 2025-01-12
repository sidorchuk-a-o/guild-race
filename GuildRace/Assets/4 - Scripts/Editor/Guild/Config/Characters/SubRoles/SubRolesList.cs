using AD.ToolsCollection;

namespace Game.Guild
{
    public class SubRolesList : ListElement<SubRoleData, SubRoleItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(SubRoleImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}