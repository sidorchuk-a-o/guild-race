using System;

namespace Game.Instances
{
    public class PowerConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(PowerConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new PowerConsumableVM(data, handler as PowerConsumableHandler, instancesVMF);
        }
    }
}