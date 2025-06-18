using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System;
using UniRx;

namespace Game.Inventory
{
    public class InventoryTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemsTooltipUIParams tooltipParams;

        private static readonly Subject<InventoryTooltipComponent> onInteracted = new();

        public static IObservable<InventoryTooltipComponent> OnInteracted => onInteracted;

        public ItemDataVM DataVM { get; private set; }
        public AssetReference TooltipRef { get; private set; }

        public void Init(ItemDataVM dataVM)
        {
            DataVM = dataVM;
            TooltipRef = tooltipParams.GetParams(dataVM.ItemType).TooltipRef;
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