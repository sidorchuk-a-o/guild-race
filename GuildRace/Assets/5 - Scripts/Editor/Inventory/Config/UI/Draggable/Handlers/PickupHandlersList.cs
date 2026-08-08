using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class PickupHandlersList : ListElement<PickupHandler>
    {
        protected override Type ItemType { get; } = typeof(PickupHandlerItem);
        protected override Type WizardType { get; } = typeof(PickupHandlerCreateWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
        }
    }
}