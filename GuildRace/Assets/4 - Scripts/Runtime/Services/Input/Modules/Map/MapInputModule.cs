using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class MapInputModule : InputModule, InputActions.IMapActions, IMapInputModule
    {
        private readonly InputActions actions;

        private readonly Subject onLeftClick = new();
        private readonly Subject onRightClick = new();
        private readonly Subject onStartScroll = new();
        private readonly Subject onStopScroll = new();

        private bool scrollStarted;

        public Vector2 CursorPosition { get; private set; }

        public IObservable OnLeftClick => onLeftClick;
        public IObservable OnRightClick => onRightClick;

        public IObservable OnStartScroll => onStartScroll;
        public IObservable OnStopScroll => onStopScroll;

        public MapInputModule(InputActions actions)
        {
            this.actions = actions;

            actions.Map.SetCallbacks(this);
        }

        public override void Enable()
        {
            base.Enable();

            actions.Map.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            actions.Map.Disable();
        }

        protected override void Update()
        {
            base.Update();

            CursorPosition = actions.Map.Cursor.ReadValue<Vector2>();
        }

        // == Callbacks ==

        void InputActions.IMapActions.OnCursor(InputAction.CallbackContext context)
        {
        }

        void InputActions.IMapActions.OnScroll(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed when !scrollStarted:
                    onStartScroll.OnNext();
                    scrollStarted = true;
                    break;
                case InputActionPhase.Canceled when scrollStarted:
                    onStopScroll.OnNext();
                    scrollStarted = false;
                    break;
            }
        }

        void InputActions.IMapActions.OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onLeftClick.OnNext();
            }
        }

        void InputActions.IMapActions.OnRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onRightClick.OnNext();
            }
        }
    }
}