using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class ChanceConsumableVMFactory : VMFactory, IConsumableMechanicVMFactory
    {
        public Type Type { get; } = typeof(ChanceConsumableHandler);

        public ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ChanceConsumableVM(data, handler as ChanceConsumableHandler, instancesVMF);
        }
    }
}