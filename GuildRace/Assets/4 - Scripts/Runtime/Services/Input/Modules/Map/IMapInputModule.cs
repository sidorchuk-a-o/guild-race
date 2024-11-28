using AD.ToolsCollection;
using UnityEngine;

namespace Game.Input
{
    public interface IMapInputModule
    {
        Vector2 CursorPosition { get; }

        IObservable OnLeftClick { get; }
        IObservable OnRightClick { get; }
        IObservable OnStartScroll { get; }
        IObservable OnStopScroll { get; }
    }
}