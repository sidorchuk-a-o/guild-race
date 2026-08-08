using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class InstanceTypesList : ListElement<InstanceTypeData>
    {
        protected override Type ItemType { get; } = typeof(InstanceTypeItem);
        protected override Type WizardType { get; } = typeof(InstanceTypeImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Instance Types";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}