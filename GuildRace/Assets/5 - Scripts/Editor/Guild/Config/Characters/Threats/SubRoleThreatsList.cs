using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class SubRoleThreatsList : ListElement<SubRoleThreatData>
    {
        protected override Type WizardType { get; } = typeof(SubRoleThreatImportWizard);
        protected override Type ItemType { get; } = typeof(SubRoleThreatItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}