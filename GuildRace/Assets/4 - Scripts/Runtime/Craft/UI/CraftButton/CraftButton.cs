using AD.UI;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using VContainer;

namespace Game.Craft
{
    public class CraftButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [Space]
        [SerializeField] private Image curencyIconImage;
        [SerializeField] private UIText priceText;
        [SerializeField] private string errorStateKey = "error";

        private readonly Subject onCraft = new();

        private IStoreService store;
        private RecipeVM recipeVM;

        public IObservable OnCraft => onCraft;

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

        public async UniTask Init(RecipeVM recipeVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.recipeVM = recipeVM;

            var currencyIcon = await recipeVM.PriceVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            curencyIconImage.sprite = currencyIcon;

            recipeVM.PriceVM.Amount
                .Subscribe(x => priceText.SetTextParams(x))
                .AddTo(disp);

            recipeVM.PriceVM.IsAvailable
                .Subscribe(UpdateButtonState)
                .AddTo(disp);
        }

        private void UpdateButtonState(bool isAvailable)
        {
            var stateKey = isAvailable ? UISelectable.defaultStateKey : errorStateKey;

            button.SetState(stateKey);
            button.SetInteractableState(isAvailable);
        }

        private void ButtonClickCallback()
        {
            var result = store.CurrenciesModule.SpendCurrency(recipeVM.PriceVM.Value);

            if (result.HasValue)
            {
                onCraft.OnNext();
            }
        }
    }
}