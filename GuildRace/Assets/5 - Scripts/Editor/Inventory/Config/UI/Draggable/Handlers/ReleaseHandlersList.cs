using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ReleaseHandlersList : ListElement<ReleaseHandler>
    {
        protected override Type ItemType { get; } = typeof(ReleaseHandlerItem);
        protected override Type WizardType { get; } = typeof(ReleaseHandlerCreateWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}