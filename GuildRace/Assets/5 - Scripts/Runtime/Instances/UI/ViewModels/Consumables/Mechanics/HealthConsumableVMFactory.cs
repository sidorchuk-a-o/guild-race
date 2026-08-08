using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class HealthConsumableVMFactory : VMFactory, IConsumableMechanicVMFactory
    {
        public Type Type { get; } = typeof(HealthConsumableHandler);

        public ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new HealthConsumableVM(data, handler as HealthConsumableHandler, instancesVMF);
        }
    }
}