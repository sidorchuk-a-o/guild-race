using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AD.UI;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.Inventory
{
    public abstract class ItemTooltipContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Item")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }

        public virtual async UniTask Init(ItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var icon = await itemVM.LoadIcon();

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(itemVM.NameKey);
        }

        public async UniTask Show(CancellationTokenSource ct)
        {
            canvasGroup.DOKill();

            await canvasGroup.DOFade(1, 0.05f).WithCancellation(ct.Token);

            if (ct.IsCancellationRequested)
            {
                canvasGroup.alpha = 0;
            }
        }

        public async UniTask Hide()
        {
            canvasGroup.DOKill();

            await canvasGroup.DOFade(0, 0.05f);
        }
    }
}