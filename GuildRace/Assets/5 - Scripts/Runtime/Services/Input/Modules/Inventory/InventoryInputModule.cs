using AD.ToolsCollection;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AD.Game.Input
{
    public class InventoryInputModule : InputModule, InputActions.IInventoryActions, IInventoryInputModule
    {
        private readonly InputActions actions;

        private readonly ReactiveProperty<bool> splittingModeOn = new();

        private readonly ReactiveEvent onLeftClick = new();
        private readonly ReactiveEvent onRightClick = new();
        private readonly ReactiveEvent onPickupItem = new();
        private readonly ReactiveEvent onReleaseItem = new();
        private readonly ReactiveEvent onRotateItem = new();
        private readonly ReactiveEvent onTryEquipItem = new();
        private readonly ReactiveEvent onTryTransferItem = new();
        private readonly ReactiveEvent onTryDeleteItem = new();
        private readonly ReactiveEvent onOpenContextMenu = new();
        private readonly ReactiveEvent onFastContextMenu = new();

        private bool pickupStarted;

        public Vector2 CursorPosition { get; private set; }
        public ReadOnlyReactiveProperty<bool> SplittingModeOn => splittingModeOn;

        public ReadOnlyReactiveEvent OnLeftClick => onLeftClick;
        public ReadOnlyReactiveEvent OnRightClick => onRightClick;

        public ReadOnlyReactiveEvent OnPickupItem => onPickupItem;
        public ReadOnlyReactiveEvent OnReleaseItem => onReleaseItem;
        public ReadOnlyReactiveEvent OnRotateItem => onRotateItem;

        public ReadOnlyReactiveEvent OnTryEquipItem => onTryEquipItem;
        public ReadOnlyReactiveEvent OnTryTransferItem => onTryTransferItem;
        public ReadOnlyReactiveEvent OnTryDeleteItem => onTryDeleteItem;

        public ReadOnlyReactiveEvent OnOpenContextMenu => onOpenContextMenu;
        public ReadOnlyReactiveEvent OnFastContextMenu => onFastContextMenu;

        public InventoryInputModule(InputActions actions)
        {
            this.actions = actions;

            actions.Inventory.SetCallbacks(this);
        }

        public override void Enable()
        {
            base.Enable();

            actions.Inventory.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            actions.Inventory.Disable();
        }

        protected override void Update()
        {
            base.Update();

            CursorPosition = actions.Inventory.Cursor.ReadValue<Vector2>();
        }

        // == Callbacks ==

        void InputActions.IInventoryActions.OnCursor(InputAction.CallbackContext context)
        {
        }

        void InputActions.IInventoryActions.OnPickup(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed when !pickupStarted:
                    onPickupItem.OnNext();
                    pickupStarted = true;
                    break;
                case InputActionPhase.Canceled when pickupStarted:
                    onReleaseItem.OnNext();
                    pickupStarted = false;
                    break;
            }
        }

        void InputActions.IInventoryActions.OnRotate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onRotateItem.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnTryEquipItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onTryEquipItem.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnTryTransferItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onTryTransferItem.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnTryDeleteItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onTryDeleteItem.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnOpenContextMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onOpenContextMenu.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnFastContextMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onFastContextMenu.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onLeftClick.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnRightClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                onRightClick.OnNext();
            }
        }

        void InputActions.IInventoryActions.OnControl(InputAction.CallbackContext context)
        {
            splittingModeOn.Value = context.phase switch
            {
                InputActionPhase.Performed => true,
                InputActionPhase.Canceled => false,
                _ => splittingModeOn.Value
            };
        }
    }
}