using AD.UI;
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

        private CraftVMFactory craftVMF;
        private RecipeVM recipeVM;

        public IObservable OnCraft => onCraft;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
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
            var craftOrder = new CraftOrderArgs
            {
                RecipeId = recipeVM.Id,
                Count = recipeVM.Count.Value
            };

            var result = craftVMF.CreateCraftOrder(craftOrder);

            if (result)
            {
                onCraft.OnNext();
            }
        }
    }
}