using UnityEngine;
using UnityEngine.AddressableAssets;
using AD.Services.Pools;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Input;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;

namespace Game.Inventory
{
    public class InventoryTooltipController : MonoBehaviour
    {
        [SerializeField] private Transform poolRoot;

        private IInventoryInputModule inputModule;
        private PoolContainer<GameObject> tooltipsPool;

        private ItemTooltipContainer currentTooltip;
        private CancellationTokenSource tooltipToken;

        private ItemsGridContainer selectedGridContainer;

        private ItemVM highlightedItemVM;
        private ItemsGridVM selectedGridVM;

        private readonly CompositeDisp tooltipDisp = new();

        [Inject]
        public void Inject(IInputService inputService, IPoolsService poolsService)
        {
            inputModule = inputService.InventoryModule;
            tooltipsPool = poolsService.CreatePrefabPool<GameObject>(poolRoot);
        }

        public void Init(CompositeDisp disp)
        {
            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            TryCloseTooltip();

            if (itemSlot && itemSlot.HasItem)
            {
                var itemVM = itemSlot.ViewModel.ItemVM.Value;
                var tooltipRef = itemSlot.GetTooltipRef();

                OpenTooltip(itemVM, tooltipRef);
            }
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            TryCloseTooltip();

            selectedGridVM = itemsGrid?.ViewModel;
            selectedGridContainer = itemsGrid;
        }

        private async void OpenTooltip(ItemVM itemVM, AssetReference tooltipRef)
        {
            tooltipDisp?.Clear();
            tooltipToken?.Cancel();

            var token = new CancellationTokenSource();

            if (itemVM == null || tooltipRef?.RuntimeKeyIsValid() == false)
            {
                return;
            }

            tooltipToken = token;

            // wait
            await UniTask.Delay(250);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // load
            var tooltipGO = await tooltipsPool.RentAsync(tooltipRef, token: token.Token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // init
            var tooltipContainer = tooltipGO.GetComponent<ItemTooltipContainer>();

            tooltipContainer.SetParent(transform);

            await tooltipContainer.Init(itemVM, tooltipDisp, token);

            await UniTask.Yield();

            if (token.IsCancellationRequested)
            {
                tooltipsPool.Return(tooltipGO);
                return;
            }

            // show
            UpdateContainerPosition(tooltipContainer);

            await tooltipContainer.Show(token);

            if (token.IsCancellationRequested)
            {
                tooltipsPool.Return(tooltipGO);
                return;
            }

            currentTooltip = tooltipContainer;
        }

        private async void TryCloseTooltip()
        {
            tooltipDisp?.Clear();
            tooltipToken?.Cancel();

            if (currentTooltip == null)
            {
                return;
            }

            var tooltip = currentTooltip;

            currentTooltip = null;

            await tooltip.Hide();

            tooltipsPool.Return(tooltip.gameObject);
        }

        private void Update()
        {
            UpdateGridItemTooltip();

            if (currentTooltip != null)
            {
                UpdateContainerPosition(currentTooltip);
            }
        }

        private void UpdateGridItemTooltip()
        {
            var gridSelected = selectedGridVM != null;

            if (gridSelected)
            {
                var positionOnGrid = RectUtils.GetPositionOnGrid(
                    cursorPosition: inputModule.CursorPosition,
                    gridContainer: selectedGridContainer,
                    itemVM: null);

                var newItemVM = selectedGridVM?.GetItem(positionOnGrid.Cursor);

                if (newItemVM != highlightedItemVM)
                {
                    TryCloseTooltip();

                    highlightedItemVM = newItemVM;

                    if (newItemVM != null)
                    {
                        var tooltipRef = selectedGridContainer.GetItemTooltip(newItemVM);

                        OpenTooltip(newItemVM, tooltipRef);
                    }
                }
            }
            else if (highlightedItemVM != null)
            {
                TryCloseTooltip();

                highlightedItemVM = null;
            }
        }

        private void UpdateContainerPosition(ItemTooltipContainer container)
        {
            var cursorPosition = inputModule.CursorPosition;
            var tooltipRect = container.transform as RectTransform;

            var offset = tooltipRect.sizeDelta / 2 + new Vector2(20, -tooltipRect.sizeDelta.y * .3f);

            tooltipRect.position = cursorPosition - offset;
            tooltipRect.ClampPositionToParent();
        }
    }
}