using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Instances
{
    public class ThreatTooltip : TooltipContainer
    {
        [Header("Threat Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var threatVM = (ThreatVM)viewModel;
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