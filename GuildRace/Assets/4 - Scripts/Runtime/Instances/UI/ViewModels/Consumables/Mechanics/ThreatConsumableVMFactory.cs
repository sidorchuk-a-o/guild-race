using System;

namespace Game.Instances
{
    public class ThreatConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(ThreatConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemInfo info, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ThreatConsumableVM(info, handler as ThreatConsumableHandler, instancesVMF);
        }
    }
}