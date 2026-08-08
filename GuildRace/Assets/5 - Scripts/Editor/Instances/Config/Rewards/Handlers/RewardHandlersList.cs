using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class RewardHandlersList : ListElement<RewardHandler>
    {
        protected override Type ItemType { get; } = typeof(RewardHandlerItem);
        protected override Type WizardType { get; } = typeof(RewardHandlerImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Reward Mechanics";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}