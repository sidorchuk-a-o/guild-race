using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ThreatsList : ListElement<ThreatData>
    {
        protected override Type ItemType { get; } = typeof(ThreatItem);
        protected override Type WizardType { get; } = typeof(ThreatDataImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Threats";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}