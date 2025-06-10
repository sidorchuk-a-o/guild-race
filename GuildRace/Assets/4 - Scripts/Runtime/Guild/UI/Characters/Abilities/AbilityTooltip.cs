using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
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
            var abilityVM = viewModel as AbilityVM;
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