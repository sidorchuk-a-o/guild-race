using AD.ToolsCollection;
using Game.UI;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

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

        private static readonly Subject<ItemsGridContainer> onInteracted = new();

        public ItemsGridVM ViewModel { get; private set; }

        public RectTransform HighlightArea => highlightArea;
        public RectTransform PickedItemArea => pickedItemArea;

        public static IObservable<ItemsGridContainer> OnInteracted => onInteracted;

        public void Init(ItemsGridVM gridVM, CompositeDisp disp)
        {
            ViewModel = gridVM;

            cellsContainer.Init(gridVM);
            itemsContainer.Init(gridVM, disp);
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