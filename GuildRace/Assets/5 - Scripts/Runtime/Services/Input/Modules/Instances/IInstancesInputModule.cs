using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Input
{
    public interface IInstancesInputModule
    {
        Vector2 CursorPosition { get; }

        ReadOnlyReactiveEvent OnLeftClick { get; }
        ReadOnlyReactiveEvent OnRightClick { get; }

        ReadOnlyReactiveEvent OnPickup { get; }
        ReadOnlyReactiveEvent OnRelease { get; }

        ReadOnlyReactiveEvent OnStartScroll { get; }
        ReadOnlyReactiveEvent OnStopScroll { get; }
    }
}