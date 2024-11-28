using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
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

        private InstancesVMFactory instancesVMF;
        private IObjectResolver resolver;

        private InstanceVM instanceVM;

        private InstanceMapUIComponent instanceMap;
        private AddressableGameObject instanceMapRef;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF, IObjectResolver resolver)
        {
            this.instancesVMF = instancesVMF;
            this.resolver = resolver;

            instanceVM = instancesVMF.GetCurrentInstance();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            instanceVM.AddTo(disp);

            // header

            headerText.SetTextParams(instanceVM.NameKey);

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

            instanceMapRef = instanceVM.UIRef;
            instanceMapRef.SetParent(mapScrollRect.content);

            var instanceMapGO = await instanceMapRef.LoadAsync();

            resolver.InjectGameObject(instanceMapGO);

            instanceMap = instanceMapGO.GetComponent<InstanceMapUIComponent>();
            instanceMap.Init(disp);
        }

        // == Destroy ==

        private void OnDestroy()
        {
            instanceMapRef?.Release();
        }
    }
}