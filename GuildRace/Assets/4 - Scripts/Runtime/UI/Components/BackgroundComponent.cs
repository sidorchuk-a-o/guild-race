using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Image), typeof(AspectRatioFitter))]
    public class BackgroundComponent : MonoBehaviour
    {
        [SerializeField] private FillMode fillMode = FillMode.Auto;

        private Image image;
        private RectTransform parentRectTransform;
        private AspectRatioFitter aspectRatioFitter;

        private void Awake()
        {
            image = GetComponent<Image>();
            aspectRatioFitter = GetComponent<AspectRatioFitter>();
            parentRectTransform = transform.parent as RectTransform;
        }

        private void Start()
        {
            UpdateBackground();
        }

        private void OnCanvasGroupChanged()
        {
            UpdateBackground();
        }

        private void OnRectTransformDimensionsChange()
        {
            UpdateBackground();
        }

        public void SetFillMode(FillMode mode)
        {
            fillMode = mode;

            UpdateBackground();
        }

        public void UpdateBackground()
        {
            if (image == null || image.sprite == null) return;

            var spriteAspect = image.sprite.textureRect.width / image.sprite.textureRect.height;
            var rectAspect = parentRectTransform.rect.width / parentRectTransform.rect.height;

            aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.None;

            switch (fillMode)
            {
                case FillMode.FitWidth:
                    aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    aspectRatioFitter.aspectRatio = spriteAspect;
                    break;

                case FillMode.FitHeight:
                    aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
                    aspectRatioFitter.aspectRatio = spriteAspect;
                    break;

                case FillMode.Auto:
                    if (rectAspect > spriteAspect)
                    {
                        aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
                    }
                    else
                    {
                        aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    }
                    aspectRatioFitter.aspectRatio = spriteAspect;
                    break;
            }

            image.preserveAspect = true;
        }

        public enum FillMode
        {
            FitWidth,
            FitHeight,
            Auto
        }
    }
}