using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;
using Game.Instances;
using UnityEngine;

namespace Game.Leaderboards
{
    public class RankRewardTooltip : TooltipContainer
    {
        [SerializeField] private UIText descText;
        [SerializeField] private LocalizeKey defaultDescKey;
        [SerializeField] private LocalizeKey rewardedDescKey;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var unitVM = viewModel as UnitVM;

            var descKey = unitVM.CompletedCount.Value > 0
                ? rewardedDescKey
                : defaultDescKey;

            descText.SetTextParams(descKey);

            return UniTask.CompletedTask;
        }
    }
}