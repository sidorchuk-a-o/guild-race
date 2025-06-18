using System;

namespace Game.Instances
{
    public class ChanceConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(ChanceConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ChanceConsumableVM(data, handler as ChanceConsumableHandler, instancesVMF);
        }
    }
}