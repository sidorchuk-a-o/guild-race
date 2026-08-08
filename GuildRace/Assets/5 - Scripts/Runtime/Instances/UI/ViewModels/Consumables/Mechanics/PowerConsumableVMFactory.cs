using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class PowerConsumableVMFactory : VMFactory, IConsumableMechanicVMFactory
    {
        public Type Type { get; } = typeof(PowerConsumableHandler);

        public ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new PowerConsumableVM(data, handler as PowerConsumableHandler, instancesVMF);
        }
    }
}