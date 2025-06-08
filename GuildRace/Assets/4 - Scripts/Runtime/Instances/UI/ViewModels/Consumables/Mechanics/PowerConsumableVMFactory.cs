using System;

namespace Game.Instances
{
    public class PowerConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(PowerConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemInfo info, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new PowerConsumableVM(info, handler as PowerConsumableHandler, instancesVMF);
        }
    }
}