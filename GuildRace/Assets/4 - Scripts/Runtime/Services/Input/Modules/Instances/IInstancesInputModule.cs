using AD.ToolsCollection;
using UnityEngine;

namespace Game.Input
{
    public interface IInstancesInputModule
    {
        Vector2 CursorPosition { get; }

        IObservable OnLeftClick { get; }
        IObservable OnRightClick { get; }

        IObservable OnPickup { get; }
        IObservable OnRelease { get; }

        IObservable OnStartScroll { get; }
        IObservable OnStopScroll { get; }
    }
}