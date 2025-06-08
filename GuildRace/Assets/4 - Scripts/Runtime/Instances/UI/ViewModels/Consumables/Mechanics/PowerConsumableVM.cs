namespace Game.Instances
{
    public class PowerConsumableVM : ConsumableMechanicVM
    {
        public int Power { get; }

        public PowerConsumableVM(ConsumablesItemInfo info, PowerConsumableHandler handler, InstancesVMFactory instancesVMF) 
            : base(info, handler, instancesVMF)
        {
            Power = handler.GetPower(info);
        }
    }
}