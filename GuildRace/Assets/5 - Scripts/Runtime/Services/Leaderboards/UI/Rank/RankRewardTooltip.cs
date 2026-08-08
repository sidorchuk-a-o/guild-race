using System.Threading;
using AD.Game.Instances;
using AD.Game.UI;
using AD.Localization;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Leaderboards
{
    public class RankRewardTooltip : TooltipContainer
    {
        [SerializeField] private UIText descText;
        [SerializeField] private LocalizeKey defaultDescKey;
        [SerializeField] private LocalizeKey rewardedDescKey;

        public override UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var unitVM = (UnitVM)viewModel;

            var descKey = unitVM.CompletedCount.CurrentValue > 0
                ? rewardedDescKey
                : defaultDescKey;

            descText.SetTextParams(descKey);

            return UniTask.CompletedTask;
        }
    }
}