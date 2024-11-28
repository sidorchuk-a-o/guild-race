namespace Game.Input
{
    public interface IInputService
    {
        IInventoryInputModule InventoryModule { get; }
        IMapInputModule MapModule { get; }
    }
}