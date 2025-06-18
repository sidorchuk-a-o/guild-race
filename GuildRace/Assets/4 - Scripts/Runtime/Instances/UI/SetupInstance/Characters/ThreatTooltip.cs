using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class ThreatTooltip : TooltipContainer
    {
        [Header("Threat Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var threatVM = viewModel as ThreatVM; 
            var icon = await threatVM.ThreatDataVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(threatVM.ThreatDataVM.NameKey);
            descText.SetTextParams(threatVM.ThreatDataVM.DescKey);
        }
    }
}