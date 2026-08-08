using AD.ToolsCollection;

namespace AD.Game.Input
{
    public interface IInputModule
    {
        bool Enabled { get; }

        ReadOnlyReactiveEvent OnEnabled { get; }
        ReadOnlyReactiveEvent OnDisabled { get; }
    }
}