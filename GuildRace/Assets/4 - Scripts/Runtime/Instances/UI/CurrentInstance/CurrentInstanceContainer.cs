using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Instances
{
    public class CurrentInstanceContainer : UIContainer
    {
        [Header("Header")]
        [SerializeField] private UIText headerText;

        [Header("Map")]
        [SerializeField] private InstanceMapScrollRect mapScrollRect;

        [Header("Buttons")]
        [SerializeField] private UIButton stopInstanceButton;

        private InstancesVMFactory instancesVMF;
        private IObjectResolver resolver;

        private ActiveInstanceVM activeInstanceVM;

        private InstanceMapUIComponent instanceMap;
        private AddressableGameObject instanceMapRef;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF, IObjectResolver resolver)
        {
            this.instancesVMF = instancesVMF;
            this.resolver = resolver;
        }

        private void Awake()
        {
            stopInstanceButton.OnClick
                .Subscribe(StopInstanceCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            activeInstanceVM = instancesVMF.GetPlayerInstance();
            activeInstanceVM.AddTo(disp);

            // header

            headerText.SetTextParams(activeInstanceVM.InstanceVM.NameKey);

            // map

            await LoadMap(disp);

            mapScrollRect.Init(disp);
            mapScrollRect.SnapTo(instanceMap.EntryPoint);
        }

        private async UniTask LoadMap(CompositeDisp disp)
        {
            if (instanceMapRef != null)
            {
                await instanceMapRef.ReleaseAsync();
            }

            instanceMapRef = activeInstanceVM.InstanceVM.UIRef;
            instanceMapRef.SetParent(mapScrollRect.content);

            var instanceMapGO = await instanceMapRef.LoadAsync();

            resolver.InjectGameObject(instanceMapGO);

            instanceMap = instanceMapGO.GetComponent<InstanceMapUIComponent>();
            instanceMap.Init(disp);
        }

        private async void StopInstanceCallback()
        {
            await instancesVMF.StopPlayerInstance();
        }

        // == Destroy ==

        private void OnDestroy()
        {
            instanceMapRef?.Release();
        }
    }
}