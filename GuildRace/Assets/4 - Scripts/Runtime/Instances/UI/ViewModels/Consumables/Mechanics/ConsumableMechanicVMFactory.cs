using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    public abstract class ConsumableMechanicVMFactory : ScriptableData
    {
        public abstract Type Type { get; }

        public abstract ConsumableMechanicVM GetValue(
            ConsumablesItemInfo info,
            ConsumableMechanicHandler handler,
            InstancesVMFactory instancesVMF);
    }
}