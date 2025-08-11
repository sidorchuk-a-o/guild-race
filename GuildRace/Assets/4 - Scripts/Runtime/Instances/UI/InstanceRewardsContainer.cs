using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Ads;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class InstanceRewardsContainer : UIContainer
    {
        [Header("Rewards")]
        [SerializeField] private RewardsContainer rewardsContainer;
        [SerializeField] private AdsRewardsContainer adsRewardsContainer;
        [Space]
        [SerializeField] private UIButton takeRewardsButton;
        [SerializeField] private AdsButton adsRewardButton;

        private InstancesVMFactory instancesVMF;
        private ActiveInstanceVM instanceVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            takeRewardsButton.OnClick
                .Subscribe(TakeRewardCallback)
                .AddTo(this);

            adsRewardButton.OnRewarded
                .Subscribe(TakeAdsRewardCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            instanceVM = instancesVMF.GetCompletedInstance();
            instanceVM.AddTo(disp);

            rewardsContainer.Init(instanceVM.RewardsVM, ct);
            adsRewardsContainer.Init(instanceVM.AdsRewardVM, disp, ct);

            adsRewardButton.Init(disp);

            adsRewardButton.IsCompleted
                .Subscribe(UpdateAdsRewardState)
                .AddTo(disp);
        }

        private void TakeRewardCallback()
        {
            Router.Push(RouteKeys.Hub.ActiveInstances);
        }

        private void UpdateAdsRewardState()
        {
            var hasAdsRewards = instanceVM.AdsRewardVM.Count > 0;
            var adsIsCompleted = adsRewardButton.IsCompleted.Value;

            adsRewardButton.SetActive(!adsIsCompleted && hasAdsRewards);
        }

        private void TakeAdsRewardCallback()
        {
            instancesVMF.ReceiveAdsRewards();

            adsRewardButton.SetActive(false);
        }
    }
}