using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SetupInstanceContainer : UIContainer
    {
        [Header("Header")]
        [SerializeField] private UIText headerText;
        [SerializeField] private LocalizeKey headerKey;

        [Header("Button")]
        [SerializeField] private UIButton backButton;

        public const string seasonKey = "season_id";
        public const string instanceKey = "instance_id";

        private InstancesVMFactory instancesVMF;
        private InstanceVM instanceVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            backButton.OnClick
                .Subscribe(BackCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            parameters.TryGetRouteValue<int>(seasonKey, out var seasonId);
            parameters.TryGetRouteValue<int>(instanceKey, out var instanceId);

            instanceVM = instancesVMF.GetInstance(seasonId, instanceId);
            instanceVM.AddTo(disp);

            // upd params

            headerText.SetTextParams(new(headerKey, instanceVM.NameKey));
        }

        private void BackCallback()
        {
            Router.Back(LoadingScreenKeys.loading);
        }
    }
}