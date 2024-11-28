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
        [SerializeField] private UIButton startButton;

        public const string instanceKey = "instance_id";

        private InstancesVMFactory instancesVMF;

        private int instanceId;
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

            startButton.OnClick
                .Subscribe(StartCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            parameters.TryGetRouteValue(instanceKey, out instanceId);

            instanceVM = instancesVMF.GetInstance(instanceId);
            instanceVM.AddTo(disp);

            // upd params

            headerText.SetTextParams(new(headerKey, instanceVM.NameKey));
        }

        private void BackCallback()
        {
            Router.Back(LoadingScreenKeys.loading);
        }

        private async void StartCallback()
        {
            SetButtonsState(false);

            await instancesVMF.StartInstance(instanceId);

            SetButtonsState(true);
        }

        private void SetButtonsState(bool state)
        {
            backButton.SetInteractableState(state);
            startButton.SetInteractableState(state);
        }
    }
}