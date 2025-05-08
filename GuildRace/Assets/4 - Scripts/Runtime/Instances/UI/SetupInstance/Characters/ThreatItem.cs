#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class ThreatItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private GameObject resolvedIndicator;

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
        }
    }
}