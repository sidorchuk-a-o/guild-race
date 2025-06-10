using System;
using AD.Services.Router;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private AssetReference tooltipRef;

        private static readonly Subject<TooltipComponent> onInteracted = new();

        public static IObservable<TooltipComponent> OnInteracted => onInteracted;

        public ViewModel ViewModel { get; private set; }
        public AssetReference TooltipRef => tooltipRef;

        public void Init(ViewModel viewModel)
        {
            ViewModel = viewModel;
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