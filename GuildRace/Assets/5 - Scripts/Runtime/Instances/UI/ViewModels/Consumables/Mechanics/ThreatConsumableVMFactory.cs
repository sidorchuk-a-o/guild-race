using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class ThreatConsumableVMFactory : VMFactory, IConsumableMechanicVMFactory
    {
        public Type Type { get; } = typeof(ThreatConsumableHandler);

        public ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ThreatConsumableVM(data, handler as ThreatConsumableHandler, instancesVMF);
        }
    }
}