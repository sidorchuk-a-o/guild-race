using AD.ToolsCollection;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AD.Game.Inventory
{
    public class InventoryScrollRect : ScrollRect, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly ReactiveEvent<InventoryScrollRect> onInteracted = new();

        public static ReadOnlyReactiveEvent<InventoryScrollRect> OnInteracted => onInteracted;

        // == Pointer ==

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            onInteracted.OnNext(this);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            onInteracted.OnNext(null);
        }

        // == Drag ==

        public override void OnBeginDrag(PointerEventData eventData)
        {
        }

        public override void OnDrag(PointerEventData eventData)
        {
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}