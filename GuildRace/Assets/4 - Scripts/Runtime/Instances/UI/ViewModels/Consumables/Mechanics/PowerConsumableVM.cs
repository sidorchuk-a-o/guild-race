namespace Game.Instances
{
    public class PowerConsumableVM : ConsumableMechanicVM
    {
        public int Power { get; }

        public PowerConsumableVM(ConsumablesItemData data, PowerConsumableHandler handler, InstancesVMFactory instancesVMF) 
            : base(data, handler, instancesVMF)
        {
            Power = handler.GetPower(data);
        }
    }
}