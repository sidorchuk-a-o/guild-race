using System;

namespace Game.Instances
{
    public class HealthConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(HealthConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemInfo info, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new HealthConsumableVM(info, handler as HealthConsumableHandler, instancesVMF);
        }
    }
}