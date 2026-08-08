using System.Threading;
using AD.Router;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game.Store
{
    public class StoreRewardsContainer : UIContainer
    {
        [Header("Rewards")]
        [SerializeField] private RewardResultsContainer rewardsContainer;
        [Space]
        [SerializeField] private UIButton takeRewardsButton;

        private StoreVMFactory storeVMF;
        private RewardResultsVM rewardsVM;

        [Inject]
        public void Inject(StoreVMFactory storeVMF)
        {
            this.storeVMF = storeVMF;
        }

        private void Awake()
        {
            takeRewardsButton.OnClick
                .Subscribe(TakeRewardCallback)
                .AddTo(this);
        }

        private void OnDisable()
        {
            storeVMF.ClearRewardResults();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            rewardsVM = storeVMF.GetRewardResults();
            rewardsVM.AddTo(disp);

            rewardsContainer.Init(rewardsVM, ct);
        }

        private void TakeRewardCallback()
        {
            Router.Push(RouteKeys.Hub.Store);
        }
    }
}