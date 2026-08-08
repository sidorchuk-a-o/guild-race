using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class OptionHandlersList : ListElement<OptionHandler>
    {
        protected override Type WizardType { get; } = typeof(OptionHandlerCreateWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}