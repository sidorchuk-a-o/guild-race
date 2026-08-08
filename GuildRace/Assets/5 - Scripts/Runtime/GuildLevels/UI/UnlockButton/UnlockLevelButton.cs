using System.Threading;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.GuildLevels
{
    public class UnlockLevelButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [Space]
        [SerializeField] private Image currencyIconImage;
        [SerializeField] private UIText priceText;
        [SerializeField] private string errorStateKey = "error";

        private readonly ReactiveEvent onPurchased = new();

        private IStoreService store;
        private LevelVM levelVM;

        public ReadOnlyReactiveEvent OnUnlock => onPurchased;

        [Inject]
        public void Inject(IStoreService store)
        {
            this.store = store;
        }

        private void Awake()
        {
            button.OnClick
                .Subscribe(ButtonClickCallback)
                .AddTo(this);
        }

        public async UniTask Init(LevelVM levelVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.levelVM = levelVM;

            var currencyIcon = await levelVM.UnlockPriceVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            currencyIconImage.sprite = currencyIcon;

            levelVM.UnlockPriceVM.Amount
                .Subscribe(x => priceText.SetTextParams(x))
                .AddTo(disp);

            levelVM.UnlockPriceVM.IsAvailable
                .SilentSubscribe(UpdateButtonState)
                .AddTo(disp);

            levelVM.IsUnlocked
                .SilentSubscribe(UpdateButtonState)
                .AddTo(disp);

            levelVM.ReadyToUnlock
                .SilentSubscribe(UpdateButtonState)
                .AddTo(disp);

            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            var isUnlocked = levelVM.IsUnlocked.CurrentValue;
            var readyToUnlock = levelVM.ReadyToUnlock.CurrentValue;
            var isAvailable = levelVM.UnlockPriceVM.IsAvailable.CurrentValue;
            var stateKey = isAvailable ? UISelectable.DefaultStateKey : errorStateKey;

            button.SetState(stateKey);
            button.SetInteractableState(isAvailable && readyToUnlock && !isUnlocked);
        }

        private void ButtonClickCallback()
        {
            var price = levelVM.UnlockPriceVM.Value;
            var result = store.CurrenciesModule.SpendCurrency(price);

            if (result.HasValue)
            {
                onPurchased.OnNext();
            }
        }
    }
}