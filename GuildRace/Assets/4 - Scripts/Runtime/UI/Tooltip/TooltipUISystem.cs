using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.ToolsCollection;
using AD.Services.Pools;
using AD.Services.Router;
using Game.Input;
using Game.Inventory;
using UniRx;
using VContainer;
using UnityEngine;

namespace Game.UI
{
    public class TooltipUISystem : UIContainer
    {
        [SerializeField] private Transform poolRoot;

        private IUIInputModule uiInput;
        private PoolContainer<GameObject> tooltipsPool;

        private TooltipContainer currentTooltip;
        private CancellationTokenSource tooltipToken;
        private readonly CompositeDisp tooltipDisp = new();

        [Inject]
        public void Inject(IInputService inputService, IPoolsService poolsService)
        {
            uiInput = inputService.UIModule;
            tooltipsPool = poolsService.CreatePrefabPool<GameObject>(poolRoot);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            TooltipComponent.OnInteracted
                .Subscribe(TooltipInteractedCallback)
                .AddTo(disp);
        }

        private void TooltipInteractedCallback(TooltipComponent tooltip)
        {
            if (currentTooltip != null)
            {
                CloseTooltip();
            }

            OpenTooltip(tooltip);
        }

        private async void OpenTooltip(TooltipComponent tooltipComponent)
        {
            tooltipDisp?.Clear();
            tooltipToken?.Cancel();

            if (tooltipComponent == null)
            {
                return;
            }

            var token = new CancellationTokenSource();
            var viewModel = tooltipComponent.ViewModel;
            var tooltipRef = tooltipComponent.TooltipRef;

            tooltipToken = token;

            // wait
            await UniTask.Delay(250);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // load
            var tooltipGO = await tooltipsPool.RentAsync(tooltipRef, token: token.Token);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // init
            var tooltipContainer = tooltipGO.GetComponent<TooltipContainer>();

            tooltipContainer.SetParent(transform);
            tooltipContainer.transform.localScale = Vector3.one;

            await tooltipContainer.Init(viewModel, tooltipDisp, token);

            await UniTask.Yield();

            if (token.IsCancellationRequested)
            {
                tooltipsPool.Return(tooltipGO);
                return;
            }

            // show
            UpdateContainerPosition(tooltipContainer);

            await tooltipContainer.Show(token);

            if (token.IsCancellationRequested)
            {
                tooltipsPool.Return(tooltipGO);
                return;
            }

            currentTooltip = tooltipContainer;
        }

        private async void CloseTooltip()
        {
            tooltipDisp?.Clear();
            tooltipToken?.Cancel();

            if (currentTooltip == null)
            {
                return;
            }

            var tooltip = currentTooltip;

            currentTooltip = null;

            await tooltip.Hide();

            tooltipsPool.Return(tooltip.gameObject);
        }

        private void Update()
        {
            if (currentTooltip == null)
            {
                return;
            }

            UpdateContainerPosition(currentTooltip);
        }

        private void UpdateContainerPosition(TooltipContainer container)
        {
            var cursorPosition = uiInput.CursorPosition;
            var tooltipRect = container.transform as RectTransform;

            var offset = tooltipRect.sizeDelta / 2 + new Vector2(20, -tooltipRect.sizeDelta.y * .3f);

            tooltipRect.position = cursorPosition - offset;
            tooltipRect.ClampPositionToParent();
        }
    }
}