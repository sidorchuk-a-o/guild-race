using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class AdsRewardItem : MonoBehaviour
    {
        [SerializeField] private RewardItem rewardItem;
        [Space]
        [SerializeField] private UIStates rewardedState;
        [SerializeField] private string notRewardedKey = "default";
        [SerializeField] private string rewardedKey = "rewarded";

        public void Init(AdsInstanceRewardVM rewardVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            rewardItem.Init(rewardVM.RewardVM, ct);

            rewardVM.IsRewarded
                .Subscribe(x => rewardedState.SetState(x ? rewardedKey : notRewardedKey))
                .AddTo(disp);
        }
    }
}