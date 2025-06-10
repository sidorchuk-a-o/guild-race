namespace Game.Instances
{
    public class ChanceConsumableVM : ConsumableMechanicVM
    {
        public int Chance { get; }

        public ChanceConsumableVM(ConsumablesItemInfo info, ChanceConsumableHandler handler, InstancesVMFactory instancesVMF) 
            : base(info, handler, instancesVMF)
        {
            Chance = handler.GetChance(info);
        }
    }
}