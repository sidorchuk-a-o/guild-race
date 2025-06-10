namespace Game.Input
{
    public interface IInputService
    {
        IUIInputModule UIModule { get; }
        IInventoryInputModule InventoryModule { get; }
        IInstancesInputModule InstancesModule { get; }
    }
}