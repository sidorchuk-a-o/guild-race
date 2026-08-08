using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class RolesList : ListElement<RoleData>
    {
        protected override Type WizardType { get; } = typeof(RoleImportWizard);
        protected override Type ItemType { get; } = typeof(RoleItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}