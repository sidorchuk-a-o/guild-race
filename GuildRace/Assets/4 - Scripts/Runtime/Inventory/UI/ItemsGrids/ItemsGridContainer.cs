using AD.ToolsCollection;
using Game.UI;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public class ItemsGridContainer : UIComponent<ItemsGridContainer>, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Containers")]
        [SerializeField] private GridCellsContainer cellsContainer;
        [SerializeField] private ItemsPreviewContainer itemsContainer;

        [Header("Areas")]
        [SerializeField] private RectTransform highlightArea;
        [SerializeField] private RectTransform pickedItemArea;

        [Header("Params")]
        [SerializeField] private ItemsGridUIParams gridParams;
        [SerializeField] private ItemsTooltipUIParams tooltipParams;

        private static readonly Subject<ItemsGridContainer> onInited = new();
        private static readonly Subject<ItemsGridContainer> onInteracted = new();

        public ItemsGridVM ViewModel { get; private set; }

        public RectTransform HighlightArea => highlightArea;
        public RectTransform PickedItemArea => pickedItemArea;

        public static IObservable<ItemsGridContainer> OnInited => onInited;
        public static IObservable<ItemsGridContainer> OnInteracted => onInteracted;

        public int CellSize => gridParams.CellSize;

        public void Init(ItemsGridVM gridVM, CompositeDisp disp)
        {
            ViewModel = gridVM;

            cellsContainer.Init(gridVM, CellSize);
            itemsContainer.Init(gridVM, gridParams, disp);

            UpdateSize();

            onInited.OnNext(this);
        }

        private void UpdateSize()
        {
            var rect = transform as RectTransform;

            rect.sizeDelta = new Vector2
            {
                x = ViewModel.Bounds.Value.size.x * CellSize,
                y = ViewModel.Bounds.Value.size.y * CellSize
            };
        }

        public AssetReference GetItemTooltip(ItemVM itemVM)
        {
            var itemType = itemVM.DataVM.ItemType;
            var itemParams = tooltipParams.GetParams(itemType);

            return itemParams.TooltipRef;
        }

        // == IPointer ==

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            onInteracted.OnNext(this);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            onInteracted.OnNext(null);
        }
    }
}