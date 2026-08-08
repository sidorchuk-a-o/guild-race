using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ConsumableMechanicHandlersList : ListElement<ConsumableMechanicHandler>
    {
        protected override Type ItemType { get; } = typeof(ConsumableMechanicHandlerItem);
        protected override Type WizardType { get; } = typeof(ConsumableMechanicHandlerImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Consumable Mechanics";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}