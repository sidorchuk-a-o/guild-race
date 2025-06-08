namespace Game.Instances
{
    public class HealthConsumableVM : ConsumableMechanicVM
    {
        public int Health { get; }

        public HealthConsumableVM(ConsumablesItemInfo info, HealthConsumableHandler handler, InstancesVMFactory instancesVMF) 
            : base(info, handler, instancesVMF)
        {
            Health = handler.GetHealth(info);
        }
    }
}