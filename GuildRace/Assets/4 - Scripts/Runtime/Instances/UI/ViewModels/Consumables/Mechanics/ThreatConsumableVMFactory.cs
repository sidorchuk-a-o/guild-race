using System;

namespace Game.Instances
{
    public class ThreatConsumableVMFactory : ConsumableMechanicVMFactory
    {
        public override Type Type { get; } = typeof(ThreatConsumableHandler);

        public override ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ThreatConsumableVM(data, handler as ThreatConsumableHandler, instancesVMF);
        }
    }
}