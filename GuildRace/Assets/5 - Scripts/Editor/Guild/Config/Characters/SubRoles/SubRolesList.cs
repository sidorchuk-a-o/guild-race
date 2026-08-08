using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class SubRolesList : ListElement<SubRoleData>
    {
        protected override Type WizardType { get; } = typeof(SubRoleImportWizard);
        protected override Type ItemType { get; } = typeof(SubRoleItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}