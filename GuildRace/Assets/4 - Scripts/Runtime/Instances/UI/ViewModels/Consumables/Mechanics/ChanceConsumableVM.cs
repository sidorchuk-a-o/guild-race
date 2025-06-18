namespace Game.Instances
{
    public class ChanceConsumableVM : ConsumableMechanicVM
    {
        public int Chance { get; }

        public ChanceConsumableVM(ConsumablesItemData data, ChanceConsumableHandler handler, InstancesVMFactory instancesVMF) 
            : base(data, handler, instancesVMF)
        {
            Chance = handler.GetChance(data);
        }
    }
}