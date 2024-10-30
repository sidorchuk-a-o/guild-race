using AD.ToolsCollection;

namespace Game.Input
{
    public interface IInputModule
    {
        bool Enabled { get; }

        IObservable OnEnabled { get; }
        IObservable OnDisabled { get; }
    }
}