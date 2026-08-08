using System;

namespace AD.Game.Instances
{
    public interface IConsumableMechanicVMFactory
    {
        Type Type { get; }

        ConsumableMechanicVM GetValue(ConsumablesItemData data, ConsumableMechanicHandler handler, InstancesVMFactory instancesVMF);
    }
}