using Game.Components;
using UnityEngine;

namespace Game
{
    public class CameraComponent : GameComponent<CameraComponent>
    {
        [SerializeField] private Camera uiCamera;

        public Camera UICamera => uiCamera;
    }
}