using AD.ToolsCollection;
using Game.Input;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryScrollController : MonoBehaviour
    {
        [Header("Auto Scroll Params")]
        [SerializeField] private float holdScrollPadding = 85f;

        [Header("Draggable")]
        [SerializeField] private InventoryDraggableController draggableController;

        private IInventoryInputModule inventoryInputs;

        private InventoryScrollRect selectedScrollRect;
        private InventoryScrollViewRect selectedScrollViewRect;

        private ItemVM selectedItemVM;

        [Inject]
        public void Inject(IInputService inputService)
        {
            inventoryInputs = inputService.InventoryModule;
        }

        public void Init(CompositeDisp disp)
        {
            draggableController.OnPickupItem
                .Subscribe(PickupItemCallback)
                .AddTo(disp);

            draggableController.OnReleaseItem
                .Subscribe(ReleaseItemCallback)
                .AddTo(disp);

            InventoryScrollRect.OnInteracted
                .Subscribe(ScrollRectInteractedCallback)
                .AddTo(disp);

            InventoryScrollViewRect.OnInteracted
                .Subscribe(ScrollViewRectInteractedCallback)
                .AddTo(disp);
        }

        private void PickupItemCallback(PickupResult result)
        {
            selectedItemVM = result.SelectedItemVM;
        }

        private void ReleaseItemCallback(ReleaseResult _)
        {
            selectedItemVM = null;
        }

        private void ScrollRectInteractedCallback(InventoryScrollRect scrollRect)
        {
            selectedScrollRect = scrollRect;
        }

        private void ScrollViewRectInteractedCallback(InventoryScrollViewRect scrollRect)
        {
            selectedScrollViewRect = scrollRect;
        }

        private void Update()
        {
            if (selectedItemVM == null)
            {
                return;
            }

            ScrollRect();

            ScrollViewRect();
        }

        private void ScrollRect()
        {
            if (selectedScrollRect == null)
            {
                return;
            }

            var viewport = selectedScrollRect.viewport;
            var viewportRect = viewport.rect;

            var cursorPosition = inventoryInputs.CursorPosition;
            var positionOnViewport = RectUtils.GetLocalPosition(viewport, cursorPosition);

            var scrollValue = selectedScrollRect.verticalNormalizedPosition * viewportRect.height;

            if (positionOnViewport.y < viewportRect.min.y + holdScrollPadding)
            {
                scrollValue -= selectedScrollRect.scrollSensitivity * 50 * Time.deltaTime;

                selectedScrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollValue / viewportRect.height);
            }

            if (positionOnViewport.y > viewportRect.max.y - holdScrollPadding)
            {
                scrollValue += selectedScrollRect.scrollSensitivity * 50 * Time.deltaTime;

                selectedScrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollValue / viewportRect.height);
            }
        }

        private void ScrollViewRect()
        {
            if (selectedScrollViewRect == null)
            {
                return;
            }

            var viewport = selectedScrollViewRect.Viewport;
            var viewportRect = viewport.rect;

            var cursorPosition = inventoryInputs.CursorPosition;
            var positionOnViewport = RectUtils.GetLocalPosition(viewport, cursorPosition);

            var scrollValue = (float)selectedScrollViewRect.GetNormalizedPosition() * viewportRect.height;

            if (positionOnViewport.y < viewportRect.min.y + holdScrollPadding)
            {
                scrollValue -= selectedScrollViewRect.ScrollSensitivity * Time.deltaTime;

                selectedScrollViewRect.SetNormalizedPosition(Mathf.Clamp01(scrollValue / viewportRect.height));
            }

            if (positionOnViewport.y > viewportRect.max.y - holdScrollPadding)
            {
                scrollValue += selectedScrollViewRect.ScrollSensitivity * Time.deltaTime;

                selectedScrollViewRect.SetNormalizedPosition(Mathf.Clamp01(scrollValue / viewportRect.height));
            }
        }
    }
}