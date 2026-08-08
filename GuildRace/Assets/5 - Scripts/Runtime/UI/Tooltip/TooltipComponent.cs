using AD.Router;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

namespace AD.Game.UI
{
    public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private AssetReference tooltipRef;

        private bool interacted;

        private static readonly ReactiveEvent<TooltipComponent> onInteracted = new();

        public static ReadOnlyReactiveEvent<TooltipComponent> OnInteracted => onInteracted;

        public ViewModel ViewModel { get; private set; }
        public AssetReference TooltipRef => tooltipRef;

        public void Init(ViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        // == IPointer ==

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            interacted = true;
            onInteracted.OnNext(this);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            interacted = false;
            onInteracted.OnNext(null);
        }

        private void OnDisable()
        {
            if (interacted)
            {
                onInteracted.OnNext(null);
            }
        }
    }
}