using System;
using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class InstanceRewardsList : ListElement<InstanceRewardData>
    {
        protected override Type ItemType { get; } = typeof(InstanceRewardItem);
        protected override Type WizardType { get; } = typeof(InstanceRewardImportWizard);

        protected override List<Header> Headers => new()
        {
            new("Id", 70),
            new("Boss", 200),
            new("Mech", 120),
            new("Mech Params")
        };

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}