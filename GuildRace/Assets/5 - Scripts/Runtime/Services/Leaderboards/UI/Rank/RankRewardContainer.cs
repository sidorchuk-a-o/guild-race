using AD.Game.Instances;
using AD.Game.UI;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Leaderboards
{
    public class RankRewardContainer : MonoBehaviour
    {
        [SerializeField] private UIStates rewardState;
        [SerializeField] private string rewardedKey = "rewarded";
        [Space]
        [SerializeField] private TooltipComponent tooltipComponent;

        public void Init(UnitVM unitVM, CompositeDisp disp)
        {
            unitVM.CompletedCount
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            tooltipComponent.Init(unitVM);
        }

        private void CountChangedCallback(int count)
        {
            var stateName = count > 0
                ? rewardedKey
                : UISelectable.DefaultStateKey;

            rewardState.SetState(stateName);
        }
    }
}