using UniRx;
using UnityEngine.InputSystem;
using UnityEngine;
using AD.ToolsCollection;

namespace Game.Input
{
    public class InventoryInputModule : InputModule, InputActions.IInventoryActions, IInventoryInputModule
    {
        private readonly InputActions actions;

        private readonly ReactiveProperty<bool> splittingModeOn = new();

        private readonly Subject onLeftClick = new();
        private readonly Subject onRightClick = new();
        private readonly Subject onPickupItem = new();
        private readonly Subject onReleaseItem = new();
        private readonly Subject onRotateItem = new();
        private readonly Subject onTryEquipItem = new();
        private readonly Subject onTryTransferItem = new();
        private readonly Subject onTryDeleteItem = new();
        private readonly Subject onOpenContextMenu = new();
        private readonly Subject onFastContextMenu = new();

        private bool pickupStarted;

        public Vector2 CursorPosition { get; private set; }
        public IReadOnlyReactiveProperty<bool> SplittingModeOn => splittingModeOn;

        public IObservable OnLeftClick => onLeftClick;
        public IObservable OnRightClick => onRightClick;

        public IObservable OnPickupItem => onPickupItem;
        public IObservable OnReleaseItem => onReleaseItem;
        public IObservable OnRotateItem => onRotateItem;

        public IObservable OnTryEquipItem => onTryEquipItem;
        public IObservable OnTryTransferItem => onTryTransferItem;
        public IObservable OnTryDeleteItem => onTryDeleteItem;

        public IObservable OnOpenContextMenu => onOpenContextMenu;
        public IObservable OnFastContextMenu => onFastContextMenu;

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
            switch (context.phase)
            {
                case InputActionPhase.Performed: splittingModeOn.Value = true; break;
                case InputActionPhase.Canceled: splittingModeOn.Value = false; break;
            }
        }
    }
}