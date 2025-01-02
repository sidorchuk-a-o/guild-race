using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class InstancesInputModule : InputModule, InputActions.IInstancesActions, IInstancesInputModule
    {
        private readonly InputActions actions;

        private readonly Subject onLeftClick = new();
        private readonly Subject onRightClick = new();
        private readonly Subject onPickup = new();
        private readonly Subject onRelease = new();
        private readonly Subject onStartScroll = new();
        private readonly Subject onStopScroll = new();

        private bool pickupStarted;
        private bool scrollStarted;

        public Vector2 CursorPosition { get; private set; }

        public IObservable OnLeftClick => onLeftClick;
        public IObservable OnRightClick => onRightClick;

        public IObservable OnPickup => onPickup;
        public IObservable OnRelease => onRelease;

        public IObservable OnStartScroll => onStartScroll;
        public IObservable OnStopScroll => onStopScroll;

        public InstancesInputModule(InputActions actions)
        {
            this.actions = actions;

            actions.Instances.SetCallbacks(this);
        }

        public override void Enable()
        {
            base.Enable();

            actions.Instances.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            actions.Instances.Disable();
        }

        protected override void Update()
        {
            base.Update();

            CursorPosition = actions.Instances.Cursor.ReadValue<Vector2>();
        }

        // == Callbacks ==

        void InputActions.IInstancesActions.OnCursor(InputAction.CallbackContext context)
        {
        }

        void InputActions.IInstancesActions.OnPickup(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed when !pickupStarted:
                    onPickup.OnNext();
                    pickupStarted = true;
                    break;
                case InputActionPhase.Canceled when pickupStarted:
                    onRelease.OnNext();
                    pickupStarted = false;
                    break;
            }
        }

        void InputActions.IInstancesActions.OnScroll(InputAction.CallbackContext context)
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

        void InputActions.IInstancesActions.OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onLeftClick.OnNext();
            }
        }

        void InputActions.IInstancesActions.OnRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onRightClick.OnNext();
            }
        }
    }
}