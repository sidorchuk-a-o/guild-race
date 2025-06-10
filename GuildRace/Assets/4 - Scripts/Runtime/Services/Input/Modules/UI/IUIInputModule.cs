using UnityEngine;

namespace Game.Input
{
    public interface IUIInputModule
    {
        Vector2 CursorPosition { get; }
    }
}