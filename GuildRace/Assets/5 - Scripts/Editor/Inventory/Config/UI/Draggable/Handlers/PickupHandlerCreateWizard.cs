using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class PickupHandlerCreateWizard : CreateWizard
    {
        protected override Collection<Type> CreateTypesCollection()
        {
            return CollectionUtils.CreateTypesCollection<PickupHandler>();
        }
    }
}