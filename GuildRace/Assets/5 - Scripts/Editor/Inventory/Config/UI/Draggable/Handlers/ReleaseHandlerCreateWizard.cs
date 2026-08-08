using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ReleaseHandlerCreateWizard : CreateWizard
    {
        protected override Collection<Type> CreateTypesCollection()
        {
            return CollectionUtils.CreateTypesCollection<ReleaseHandler>();
        }
    }
}