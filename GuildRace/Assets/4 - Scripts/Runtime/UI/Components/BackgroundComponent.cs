using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Image), typeof(AspectRatioFitter))]
    public class BackgroundComponent : MonoBehaviour
    {
        public enum FillMode
        {
            FitWidth,
            FitHeight,
            Auto
        }

        [SerializeField] private FillMode fillMode = FillMode.Auto;

        private Image image;
        private RectTransform rectTransform;
        private AspectRatioFitter aspectRatioFitter;

        private void Awake()
        {
            image = GetComponent<Image>();
            rectTransform = GetComponent<RectTransform>();
            aspectRatioFitter = GetComponent<AspectRatioFitter>();
        }

        private void Start()
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

            var spriteAspect = image.sprite.rect.width / image.sprite.rect.height;
            var rectAspect = rectTransform.rect.width / rectTransform.rect.height;

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
    }
}