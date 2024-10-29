using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using AD.Services.Router;
using Game.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class InventoryOptionsController : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] private float containerOffset;
        [SerializeField] private OptionsContainer optionsContainer;

        private Dictionary<OptionKey, OptionHandler> optionsHandlers;

        private InventoryConfig config;
        private IRouterService router;

        private InventoryVMFactory inventoryVMF;
        private IInventoryInputModule inventoryInputs;

        private OptionsContainer selectedOptions;
        private ItemsGridContainer selectedGrid;
        private ItemSlotContainer selectedSlot;

        private ItemVM selectedItem;
        private OptionContext optionsContext;

        [Inject]
        public void Inject(
            InventoryConfig config,
            InventoryVMFactory inventoryVMF,
            IInputService inputService,
            IRouterService router,
            IObjectResolver resolver)
        {
            this.config = config;
            this.router = router;
            this.inventoryVMF = inventoryVMF;

            inventoryInputs = inputService.InventoryModule;

            optionsHandlers = config.UIParams.OptionHandlers.ToDictionary(x => x.Key, x => x);
            optionsHandlers.ForEach(x => resolver.Inject(x.Value));
        }

        public void Init(CompositeDisp disp)
        {
            optionsContainer.Init();

            inventoryInputs.OnOpenContextMenu
                .Subscribe(OpenContextMenuCallback)
                .AddTo(disp);

            inventoryInputs.OnFastContextMenu
                .Subscribe(FastContextMenuCallback)
                .AddTo(disp);

            inventoryInputs.OnTryEquipItem
                .Subscribe(x => { })
                .AddTo(disp);

            inventoryInputs.OnTryDeleteItem
                .Subscribe(TryDeleteItemCallback)
                .AddTo(disp);

            inventoryInputs.OnTryTransferItem
                .Subscribe(x => { })
                .AddTo(disp);

            inventoryInputs.OnLeftClick
                .Subscribe(LeftClickCallback)
                .AddTo(disp);

            OptionsContainer.OnInteracted
                .Subscribe(OptionsContainerInteractedCallback)
                .AddTo(disp);

            OptionButton.OnInteracted
                .Subscribe(OptionsButtonInteractedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);

            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);
        }

        private void OptionsContainerInteractedCallback(OptionsContainer optionsContainer)
        {
            selectedOptions = optionsContainer;
        }

        private void OptionsButtonInteractedCallback(OptionButton optionButton)
        {
            StartOptionProcess(optionButton.Key, optionsContext);
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer grid)
        {
            selectedGrid = grid;
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer slot)
        {
            selectedSlot = slot;
        }

        private void OpenContextMenuCallback()
        {
            selectedItem = GetItem();

            if (selectedItem == null)
            {
                CloseOptionsContainer();
                return;
            }

            OpenOptionsContainer(selectedItem);
        }

        private void FastContextMenuCallback()
        {
            var selectedItem = GetItem();

            if (selectedItem == null)
            {
                return;
            }

            var optionKey = selectedItem.GetFastOptionKey();

            var context = CreateOptionContext(selectedItem);

            StartOptionProcess(optionKey, context);
        }

        private void TryDeleteItemCallback()
        {
            var selectedItem = GetItem();

            if (selectedItem == null)
            {
                return;
            }

            var context = CreateOptionContext(selectedItem);

            StartOptionProcess(OptionKeys.Common.discard, context);
        }

        private async void StartOptionProcess(OptionKey optionKey, OptionContext context)
        {
            CloseOptionsContainer();

            if (context != null && optionKey != null)
            {
                var option = optionsHandlers[optionKey];

                await option.StartProcess(context);
            }
        }

        private void LeftClickCallback()
        {
            if (selectedOptions == null)
            {
                CloseOptionsContainer();
            }
        }

        private ItemVM GetItem()
        {
            if (selectedSlot != null && selectedSlot.HasItem)
            {
                return selectedSlot.ViewModel.ItemVM.Value;
            }

            if (selectedGrid != null)
            {
                var cursorPosition = inventoryInputs.CursorPosition;
                var gridTransform = selectedGrid.transform as RectTransform;

                var positionOnGrid = RectUtils.GetPositionOnGrid(
                    cursorPosition: cursorPosition,
                    gridTransform: gridTransform,
                    selectedItem: null);

                return selectedGrid.ViewModel.GetItem(positionOnGrid.Cursor);
            }

            return null;
        }

        private void OpenOptionsContainer(ItemVM selectedItem)
        {
            optionsContext = CreateOptionContext(selectedItem);

            var cursorPosition = inventoryInputs.CursorPosition;
            var position = cursorPosition + new Vector2(1, -1) * containerOffset;

            var options = selectedItem.GetOptionKeysPool();

            optionsContainer.SetOptions(options);
            optionsContainer.SetPosition(position);
            optionsContainer.SetActive(true);

            options.ReleaseListPool();
        }

        private OptionContext CreateOptionContext(ItemVM selectedItem)
        {
            return new OptionContext
            {
                SelectedItemId = selectedItem.Id,
                SelectedSlot = selectedSlot,
                SelectedGrid = selectedGrid
            };
        }

        private void CloseOptionsContainer()
        {
            optionsContext = null;
            selectedOptions = null;

            optionsContainer.SetActive(false);
        }
    }
}