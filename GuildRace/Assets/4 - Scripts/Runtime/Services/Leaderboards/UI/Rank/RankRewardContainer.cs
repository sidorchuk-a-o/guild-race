using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Instances;
using Game.UI;
using UniRx;
using UnityEngine;

namespace Game.Leaderboards
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
                : UISelectable.defaultStateKey;

            rewardState.SetState(stateName);
        }
    }
}