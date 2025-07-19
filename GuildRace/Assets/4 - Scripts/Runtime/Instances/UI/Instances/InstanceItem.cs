using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Instances
{
    public class InstanceItem : MonoBehaviour
    {
        [SerializeField] private Image image;
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

        public async UniTask Init(InstanceVM instanceVM, CancellationTokenSource ct)
        {
            this.instanceVM = instanceVM;

            var sprite = await instanceVM.LoadImage(ct);

            if (ct.IsCancellationRequested) return;

            image.sprite = sprite;
            nameText.SetTextParams(instanceVM.NameKey);
        }

        private async void ClickCallback()
        {
            var parameters = RouteParams.Default;

            parameters[SelectBossContainer.idKey] = instanceVM.Id;

            await router.PushAsync(RouteKeys.Hub.SelectBoss, parameters: parameters);
        }
    }
}