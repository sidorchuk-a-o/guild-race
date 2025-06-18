using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    public abstract class ConsumableMechanicVMFactory : ScriptableData
    {
        public abstract Type Type { get; }

        public abstract ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF);
    }
}