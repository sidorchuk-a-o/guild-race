using System.Threading;
using AD.Game.UI;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Instances
{
    public class ThreatItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject resolvedIndicator;
        [Space]
        [SerializeField] private TooltipComponent tooltipComponent;

        public async UniTask Init(ThreatVM threatVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var icon = await threatVM.ThreatDataVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;

            threatVM.Resolved
                .Subscribe(x => resolvedIndicator.SetActive(x))
                .AddTo(disp);

            tooltipComponent.Init(threatVM);
        }
    }
}