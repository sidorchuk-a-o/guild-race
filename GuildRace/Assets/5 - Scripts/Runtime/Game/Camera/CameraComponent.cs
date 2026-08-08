using AD.Components;
using UnityEngine;

namespace AD.Game
{
    public class CameraComponent : GameComponent<CameraComponent>
    {
        [SerializeField] private Camera uiCamera;

        public Camera UICamera => uiCamera;
    }
}