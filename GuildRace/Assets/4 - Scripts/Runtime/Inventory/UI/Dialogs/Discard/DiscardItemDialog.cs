using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class DiscardItemDialog : BaseDialog
    {
        public const string itemIdKey = "id";

        [Header("Header")]
        [SerializeField] private WindowHeaderContainer headerContainer;

        [Header("Discard Dialog")]
        [SerializeField] private UIText dialogText;
        [SerializeField] private LocalizeKey dialogKey;

        private InventoryVMFactory inventoryVMF;

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override void Awake()
        {
            base.Awake();

            headerContainer.OnClose
                .Subscribe(CancelCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            parameters.TryGetRouteValue<string>(itemIdKey, out var itemId);

            var itemVM = inventoryVMF.CreateItem(itemId);

            dialogText.SetTextParams(new(dialogKey, itemVM.DataVM.NameKey));
        }
    }
}