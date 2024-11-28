﻿using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class InstanceItem : MonoBehaviour
    {
        [SerializeField] private UIText nameText;
        [SerializeField] private UIButton button;

        private IRouterService router;

        private InstanceVM instanceVM;

        [Inject]
        public void Inject(IRouterService router)
        {
            this.router = router;
        }

        private void Awake()
        {
            button.OnClick
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public async UniTask Init(InstanceVM instanceVM)
        {
            this.instanceVM = instanceVM;

            nameText.SetTextParams(instanceVM.NameKey);
        }

        private async void ClickCallback()
        {
            var parameters = RouteParams.Default;

            parameters[SetupInstanceContainer.instanceKey] = instanceVM.Id;

            await router.PushAsync(
                pathKey: RouteKeys.Instances.setupInstance,
                loadingKey: LoadingScreenKeys.loading,
                parameters: parameters);
        }
    }
}