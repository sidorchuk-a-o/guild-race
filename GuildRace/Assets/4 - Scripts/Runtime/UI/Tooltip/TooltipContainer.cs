using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public abstract class TooltipContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }

        public abstract UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct);

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