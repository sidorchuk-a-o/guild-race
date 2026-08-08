using UnityEngine;

namespace AD.Game.Input
{
    public interface IUIInputModule
    {
        Vector2 CursorPosition { get; }
    }
}