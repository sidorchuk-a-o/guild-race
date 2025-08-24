using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Instances
{
    public class InstanceLoadingScreen : UIContainer
    {
        [SerializeField] private UIText headerText;
        [SerializeField] private Image instanceImage;

        private InstancesVMFactory instancesVMF;
        private ActiveInstanceVM instanceVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            instanceVM = instancesVMF.GetSetupInstance();
            instanceVM.AddTo(disp);

            var image = await instanceVM.InstanceVM.LoadLoadingImage(ct);

            if (ct.IsCancellationRequested) return;

            instanceImage.sprite = image;

            var instanceNameKey = instanceVM.InstanceVM.NameKey;
            var unitNameKey = instanceVM.BossUnitVM.NameKey;

            headerText.SetTextParams(new(headerText.LocalizeKey, instanceNameKey, unitNameKey));
        }
    }
}