using AD.ToolsCollection;
using Game.Input;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace Game.Instances
{
    public class InstanceMapScrollRect : ScrollRect, IPointerEnterHandler, IPointerExitHandler
    {
        private IMapInputModule mapInputs;

        private Vector2 prevCursorPosition;
        private bool scrollStarted;

        private static readonly Subject<InstanceMapScrollRect> onInteracted = new();

        public static IObservable<InstanceMapScrollRect> OnInteracted => onInteracted;

        [Inject]
        public void Inject(IInputService inputService)
        {
            mapInputs = inputService.MapModule;
        }

        public void Init(CompositeDisp disp)
        {
            mapInputs.OnStartScroll
                .Subscribe(StartScrollCallback)
                .AddTo(disp);

            mapInputs.OnStopScroll
                .Subscribe(StopScrollCallback)
                .AddTo(disp);
        }

        private void StartScrollCallback()
        {
            scrollStarted = true;
        }

        private void StopScrollCallback()
        {
            scrollStarted = false;
        }

        private void Update()
        {
            var cursorPosition = mapInputs.CursorPosition;

            if (scrollStarted)
            {
                var positionOnViewport = RectUtils.GetLocalPosition(viewport, cursorPosition);
                var prevPositionOnViewport = RectUtils.GetLocalPosition(viewport, prevCursorPosition);

                var deltaOnViewport = positionOnViewport - prevPositionOnViewport;

                verticalNormalizedPosition -= deltaOnViewport.y / content.rect.height * scrollSensitivity;
                horizontalNormalizedPosition -= deltaOnViewport.x / content.rect.width * scrollSensitivity;
            }

            prevCursorPosition = cursorPosition;
        }

        // == Snap ==

        public void SnapTo(RectTransform targetRect)
        {
            SnapTo(targetRect.position);
        }

        public void SnapTo(Vector2 position)
        {
            var viewportPosition = viewport.localPosition;
            var contentPosition = transform.InverseTransformPoint(content.position);
            var targetPosition = transform.InverseTransformPoint(position);

            content.anchoredPosition = contentPosition - targetPosition - viewportPosition;
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