using UniRx;
using UnityEngine;
using AD.ToolsCollection;

namespace Game.Input
{
    public interface IInventoryInputModule : IInputModule
    {
        Vector2 CursorPosition { get; }
        IReadOnlyReactiveProperty<bool> SplittingModeOn { get; }

        IObservable OnLeftClick { get; }
        IObservable OnRightClick { get; }

        IObservable OnPickupItem { get; }
        IObservable OnReleaseItem { get; }
        IObservable OnRotateItem { get; }

        IObservable OnTryEquipItem { get; }
        IObservable OnTryTransferItem { get; }
        IObservable OnTryDeleteItem { get; }

        IObservable OnOpenContextMenu { get; }
        IObservable OnFastContextMenu { get; }
    }
}