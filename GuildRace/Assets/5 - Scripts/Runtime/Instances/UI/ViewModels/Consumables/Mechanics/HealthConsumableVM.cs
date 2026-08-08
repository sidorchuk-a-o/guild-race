namespace AD.Game.Instances
{
    public class HealthConsumableVM : ConsumableMechanicVM
    {
        public int Health { get; }

        public HealthConsumableVM(ConsumablesItemData data, HealthConsumableHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            Health = HealthConsumableHandler.GetHealth(data);
        }
    }
}