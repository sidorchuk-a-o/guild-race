using System;

namespace Game.Instances
{
    public class ChanceConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(ChanceConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemInfo info, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ChanceConsumableVM(info, handler as ChanceConsumableHandler, instancesVMF);
        }
    }
}