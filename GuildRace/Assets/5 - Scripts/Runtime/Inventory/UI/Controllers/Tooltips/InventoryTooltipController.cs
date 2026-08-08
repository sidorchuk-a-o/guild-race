using System.Threading;
using AD.Game.Input;
using AD.Pools;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace AD.Game.Inventory
{
    public class InventoryTooltipController : MonoBehaviour
    {
        [SerializeField] private Transform poolRoot;

        [Header("Draggable")]
        [SerializeField] private InventoryDraggableController draggableController;

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

            InventoryTooltipComponent.OnInteracted
                .Subscribe(TooltipComponentInteractedCallback)
                .AddTo(this);

            draggableController.OnPickupItem
                .Subscribe(PickupItemCallback)
                .AddTo(disp);

            draggableController.OnReleaseItem
                .Subscribe(ReleaseItemCallback)
                .AddTo(disp);
        }

        private void PickupItemCallback()
        {
            TryCloseTooltip();

            enabled = false;
        }

        private void ReleaseItemCallback()
        {
            enabled = true;
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            TryCloseTooltip();

            if (enabled && itemSlot && itemSlot.HasItem)
            {
                var itemVM = itemSlot.ViewModel.ItemVM.CurrentValue;
                var tooltipRef = itemSlot.GetTooltipRef();

                OpenTooltip(new(itemVM), tooltipRef);
            }
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            TryCloseTooltip();

            selectedGridVM = itemsGrid?.ViewModel;
            selectedGridContainer = itemsGrid;
        }

        private void TooltipComponentInteractedCallback(InventoryTooltipComponent tooltipComponent)
        {
            TryCloseTooltip();

            if (enabled && tooltipComponent)
            {
                var dataVM = tooltipComponent.DataVM;
                var tooltipRef = tooltipComponent.TooltipRef;

                OpenTooltip(new(dataVM), tooltipRef);
            }
        }

        private async void OpenTooltip(TooltipContext context, AssetReference tooltipRef)
        {
            tooltipDisp?.Clear();
            tooltipToken?.Cancel();

            var token = new CancellationTokenSource();

            if (context == null || tooltipRef?.RuntimeKeyIsValid() == false)
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
            var tooltipGO = await tooltipsPool.RentAsync(tooltipRef);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // init
            var tooltipContainer = tooltipGO.GetComponent<ItemTooltipContainer>();

            tooltipContainer.SetParent(transform);
            tooltipContainer.transform.localScale = Vector3.one;

            await tooltipContainer.Init(context, tooltipDisp, token);

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

            if (currentTooltip is null)
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

            if (currentTooltip is not null)
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

                var itemVM = selectedGridVM?.GetItem(positionOnGrid.Cursor);

                if (itemVM != highlightedItemVM)
                {
                    TryCloseTooltip();

                    highlightedItemVM = itemVM;

                    if (itemVM != null)
                    {
                        var tooltipRef = selectedGridContainer.GetItemTooltip(itemVM);

                        OpenTooltip(new(itemVM), tooltipRef);
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
            var tooltipRect = (RectTransform)container.transform;

            var offset = tooltipRect.sizeDelta / 2 + new Vector2(20, -tooltipRect.sizeDelta.y * .3f);

            tooltipRect.position = cursorPosition - offset;
            tooltipRect.ClampPositionToParent();
        }
    }
}