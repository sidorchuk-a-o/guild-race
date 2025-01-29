using frame8.Logic.Misc.Visual.UI;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Inventory
{
    public class InventoryScrollViewRect : MonoBehaviour, IScrollRectProxy, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float scrollSensitivity = 1500;

        private IScrollRectProxy scrollRect;

        private static readonly Subject<InventoryScrollViewRect> onInteracted = new();

        public event Action<double> ScrollPositionChanged;

        public bool IsInitialized => scrollRect.IsInitialized;

        public bool IsHorizontal => scrollRect.IsHorizontal;
        public bool IsVertical => scrollRect.IsVertical;

        public RectTransform Content => scrollRect.Content;
        public RectTransform Viewport => scrollRect.Viewport;

        public double ContentInsetFromViewportStart => scrollRect.ContentInsetFromViewportStart;
        public double ContentInsetFromViewportEnd => scrollRect.ContentInsetFromViewportEnd;

        public float ScrollSensitivity => scrollSensitivity;

        public Vector2 Velocity
        {
            get => scrollRect.Velocity;
            set => scrollRect.Velocity = value;
        }

        public static IObservable<InventoryScrollViewRect> OnInteracted => onInteracted;

        private void Awake()
        {
            scrollRect = GetComponents<IScrollRectProxy>().FirstOrDefault(x =>
            {
                return x != (IScrollRectProxy)this;
            });

            scrollRect.ScrollPositionChanged += ScrollPositionChanged;
        }

        private void OnDestroy()
        {
            scrollRect.ScrollPositionChanged -= ScrollPositionChanged;
        }

        public double GetViewportSize()
        {
            return scrollRect.GetViewportSize();
        }

        public double GetContentSize()
        {
            return scrollRect.GetContentSize();
        }

        public double GetNormalizedPosition()
        {
            return scrollRect.GetNormalizedPosition();
        }

        public void SetNormalizedPosition(double normalizedPosition)
        {
            scrollRect.SetNormalizedPosition(normalizedPosition);
        }

        public void StopMovement()
        {
            scrollRect.StopMovement();
        }

        // == Pointer ==

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