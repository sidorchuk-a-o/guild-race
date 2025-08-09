using DG.Tweening;
using UnityEngine;

namespace Game.Tutorial
{
    public class TutorialDialog : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;

        private void Awake()
        {
            SetDisableState();
        }

        private void OnEnable()
        {
            SetEnableState();
        }

        private void OnDisable()
        {
            SetDisableState();
        }

        private void SetDisableState()
        {
            canvasGroup.DOKill();
            canvasGroup.alpha = 0;

            rectTransform.DOKill();
            rectTransform.anchoredPosition = new(0, 50);
            rectTransform.localScale = new(0.9f, 0.9f);
        }

        private void SetEnableState()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, 0.1f);

            rectTransform.DOKill();
            rectTransform.DOAnchorPos(Vector2.zero, 0.15f);
            rectTransform.DOScale(Vector2.one, 0.15f);
        }
    }
}