using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Guild
{
    public class AbilityTooltip : TooltipContainer
    {
        [Header("Ability Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;
        [SerializeField] private UIText threatNameText;
        [SerializeField] private UIText threatDescText;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var abilityVM = (AbilityVM)viewModel;
            var icon = await abilityVM.ThreatVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(abilityVM.NameKey);
            descText.SetTextParams(abilityVM.DescKey);
            threatNameText.SetTextParams(new(threatNameText.LocalizeKey, abilityVM.ThreatVM.NameKey));
            threatDescText.SetTextParams(abilityVM.ThreatVM.DescKey);
        }
    }
}