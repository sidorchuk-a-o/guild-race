using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class InstanceRewardsContainer : UIContainer
    {
        [Header("Rewards")]
        [SerializeField] private RewardsContainer rewardsContainer;
        [Space]
        [SerializeField] private UIButton takeRewardsButton;

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
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            instanceVM = instancesVMF.GetCompletedInstance();
            instanceVM.AddTo(disp);

            rewardsContainer.Init(instanceVM.RewardsVM, ct);
        }

        private void TakeRewardCallback()
        {
            Router.Push(RouteKeys.Hub.ActiveInstances);
        }
    }
}