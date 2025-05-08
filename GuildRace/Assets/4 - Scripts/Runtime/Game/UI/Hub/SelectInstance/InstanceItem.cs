#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
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

        private InstanceVM instanceVM;
        private IRouterService router;

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

            parameters[SelectBossContainer.idKey] = instanceVM.Id;

            await router.PushAsync(RouteKeys.Hub.selectBoss, parameters: parameters);
        }
    }
}