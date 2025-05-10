using AD.ToolsCollection;

namespace Game.Guild
{
    public class SubRoleThreatsList : ListElement<SubRoleThreatData, SubRoleThreatItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(SubRoleThreatImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}