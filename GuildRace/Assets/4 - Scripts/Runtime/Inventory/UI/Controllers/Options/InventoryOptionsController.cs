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

        private IInventoryInputModule inventoryInputs;

        private ItemSlotVM selectedSlotVM;
        private ItemsGridVM selectedGridVM;

        private OptionsContainer selectedOptions;
        private ItemsGridContainer selectedGridContainer;

        private ItemVM selectedItemVM;
        private OptionContext optionsContext;

        [Inject]
        public void Inject(
            InventoryConfig config,
            IInputService inputService,
            IObjectResolver resolver)
        {
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

            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);

            OptionsContainer.OnInteracted
                .Subscribe(OptionsContainerInteractedCallback)
                .AddTo(disp);

            OptionButton.OnInteracted
                .Subscribe(OptionsButtonInteractedCallback)
                .AddTo(disp);
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            selectedSlotVM = itemSlot?.ViewModel;
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            selectedGridVM = itemsGrid?.ViewModel;
            selectedGridContainer = itemsGrid;
        }

        private void OptionsContainerInteractedCallback(OptionsContainer optionsContainer)
        {
            selectedOptions = optionsContainer;
        }

        private void OptionsButtonInteractedCallback(OptionButton optionButton)
        {
            StartOptionProcess(optionButton.Key, optionsContext);
        }

        private void OpenContextMenuCallback()
        {
            selectedItemVM = GetItem();

            if (selectedItemVM == null)
            {
                CloseOptionsContainer();
                return;
            }

            OpenOptionsContainer(selectedItemVM);
        }

        private void FastContextMenuCallback()
        {
            var selectedItemVM = GetItem();

            if (selectedItemVM == null)
            {
                return;
            }

            var optionKey = selectedItemVM.GetFastOptionKey();

            var context = CreateOptionContext(selectedItemVM);

            StartOptionProcess(optionKey, context);
        }

        private void TryDeleteItemCallback()
        {
            var selectedItemVM = GetItem();

            if (selectedItemVM == null)
            {
                return;
            }

            var context = CreateOptionContext(selectedItemVM);

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
            if (selectedSlotVM != null && selectedSlotVM.HasItem)
            {
                return selectedSlotVM.ItemVM.Value;
            }

            if (selectedGridVM != null)
            {
                var positionOnGrid = RectUtils.GetPositionOnGrid(
                    cursorPosition: inventoryInputs.CursorPosition,
                    gridContainer: selectedGridContainer,
                    itemVM: null);

                return selectedGridVM.GetItem(positionOnGrid.Cursor);
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
                SelectedSlotVM = selectedSlotVM,
                SelectedGridVM = selectedGridVM
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