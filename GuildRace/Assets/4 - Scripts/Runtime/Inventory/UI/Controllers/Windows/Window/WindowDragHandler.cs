using Game.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Game.Inventory
{
    public class WindowDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform windowRect;

        private IInventoryInputModule inventoryInputs;
        private Vector2 offset;

        [Inject]
        public void Inject(IInputService inputService)
        {
            inventoryInputs = inputService.InventoryModule;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            windowRect.SetAsLastSibling();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            offset = windowRect.GetLocalPosition(inventoryInputs.CursorPosition);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            windowRect.position = inventoryInputs.CursorPosition - offset;
            windowRect.ClampPositionToParent();
        }
    }
}