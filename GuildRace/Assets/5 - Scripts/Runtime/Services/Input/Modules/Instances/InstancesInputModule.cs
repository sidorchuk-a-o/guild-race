using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AD.Game.Input
{
    public class InstancesInputModule : InputModule, InputActions.IInstancesActions, IInstancesInputModule
    {
        private readonly InputActions actions;

        private readonly ReactiveEvent onLeftClick = new();
        private readonly ReactiveEvent onRightClick = new();
        private readonly ReactiveEvent onPickup = new();
        private readonly ReactiveEvent onRelease = new();
        private readonly ReactiveEvent onStartScroll = new();
        private readonly ReactiveEvent onStopScroll = new();

        private bool pickupStarted;
        private bool scrollStarted;

        public Vector2 CursorPosition { get; private set; }

        public ReadOnlyReactiveEvent OnLeftClick => onLeftClick;
        public ReadOnlyReactiveEvent OnRightClick => onRightClick;

        public ReadOnlyReactiveEvent OnPickup => onPickup;
        public ReadOnlyReactiveEvent OnRelease => onRelease;

        public ReadOnlyReactiveEvent OnStartScroll => onStartScroll;
        public ReadOnlyReactiveEvent OnStopScroll => onStopScroll;

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