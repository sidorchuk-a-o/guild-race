using System;

namespace Game.Instances
{
    public class HealthConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(HealthConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new HealthConsumableVM(data, handler as HealthConsumableHandler, instancesVMF);
        }
    }
}