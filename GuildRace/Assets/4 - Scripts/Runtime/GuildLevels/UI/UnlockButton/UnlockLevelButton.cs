using AD.UI;
using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.GuildLevels
{
    public class UnlockLevelButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [Space]
        [SerializeField] private Image curencyIconImage;
        [SerializeField] private UIText priceText;
        [SerializeField] private string errorStateKey = "error";

        private readonly Subject onPurchased = new();

        private IStoreService store;
        private LevelVM levelVM;

        public IObservable OnUnlock => onPurchased;

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

            curencyIconImage.sprite = currencyIcon;

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
            var isUnlocked = levelVM.IsUnlocked.Value;
            var readyToUnlock = levelVM.ReadyToUnlock.Value;
            var isAvailable = levelVM.UnlockPriceVM.IsAvailable.Value;

            var stateKey = isAvailable ? UISelectable.defaultStateKey : errorStateKey;

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