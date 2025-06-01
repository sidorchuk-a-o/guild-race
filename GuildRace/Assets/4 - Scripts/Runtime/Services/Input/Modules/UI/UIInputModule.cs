using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class UIInputModule : InputModule, InputActions.IUIActions, IUIInputModule
    {
        private readonly InputActions actions;

        public Vector2 CursorPosition { get; private set; }

        public UIInputModule(InputActions actions)
        {
            this.actions = actions;

            actions.UI.SetCallbacks(this);
        }

        public override void Enable()
        {
            base.Enable();

            actions.UI.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            actions.UI.Disable();
        }

        protected override void Update()
        {
            base.Update();

            CursorPosition = actions.UI.Point.ReadValue<Vector2>();
        }

        // == Callbacks ==

        void InputActions.IUIActions.OnPoint(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnNavigate(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnClick(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnRightClick(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnSubmit(InputAction.CallbackContext context)
        {
        }

        void InputActions.IUIActions.OnCancel(InputAction.CallbackContext context)
        {
        }
    }
}