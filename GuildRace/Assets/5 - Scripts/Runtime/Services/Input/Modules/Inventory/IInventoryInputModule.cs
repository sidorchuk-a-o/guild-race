using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Input
{
    public interface IInventoryInputModule : IInputModule
    {
        Vector2 CursorPosition { get; }
        ReadOnlyReactiveProperty<bool> SplittingModeOn { get; }

        ReadOnlyReactiveEvent OnLeftClick { get; }
        ReadOnlyReactiveEvent OnRightClick { get; }

        ReadOnlyReactiveEvent OnPickupItem { get; }
        ReadOnlyReactiveEvent OnReleaseItem { get; }
        ReadOnlyReactiveEvent OnRotateItem { get; }

        ReadOnlyReactiveEvent OnTryEquipItem { get; }
        ReadOnlyReactiveEvent OnTryTransferItem { get; }
        ReadOnlyReactiveEvent OnTryDeleteItem { get; }

        ReadOnlyReactiveEvent OnOpenContextMenu { get; }
        ReadOnlyReactiveEvent OnFastContextMenu { get; }
    }
}