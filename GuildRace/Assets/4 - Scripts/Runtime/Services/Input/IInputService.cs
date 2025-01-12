namespace Game.Input
{
    public interface IInputService
    {
        IInventoryInputModule InventoryModule { get; }
        IInstancesInputModule InstancesModule { get; }
    }
}