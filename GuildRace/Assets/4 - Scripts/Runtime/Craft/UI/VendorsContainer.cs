using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Craft
{
    public class VendorsContainer : UIContainer
    {
        [Header("Vendors")]
        [SerializeField] private VendorsTabsContainer vendorsTabsContainer;

        [Header("Selected Recipe")]
        [SerializeField] private CanvasGroup recipeContainer;
        [SerializeField] private CanvasGroup emptyRecipeContainer;
        [Space]
        [SerializeField] private UIText recipeNameText;
        [SerializeField] private ProductContainer productContainer;
        [SerializeField] private IngredientsContainer ingredientsContainer;
        [SerializeField] private CraftingCounterContainer counterContainer;
        [Space]
        [SerializeField] private UIButton startCraftingButton;

        private readonly CompositeDisp recipeDisp = new();
        private CancellationTokenSource recipeToken;

        private int? lastRecipeId;
        private CraftVMFactory craftVMF;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        private void Awake()
        {
            startCraftingButton.OnClick
                .Subscribe(StartCraftingCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            vendorsTabsContainer.Init(disp);

            vendorsTabsContainer.RecipeVM
                .Subscribe(RecipeChangedCallback)
                .AddTo(disp);
        }

        private async void RecipeChangedCallback(RecipeVM recipeVM)
        {
            recipeDisp.Clear();
            recipeDisp.AddTo(disp);

            var hasRecipe = recipeVM != null;
            var token = new CancellationTokenSource();

            recipeToken?.Cancel();
            recipeToken = token;

            if (hasRecipe == false &&
                lastRecipeId.HasValue == false)
            {
                recipeContainer.alpha = 0;
                recipeContainer.interactable = false;
                return;
            }

            if (hasRecipe &&
                lastRecipeId.HasValue &&
                lastRecipeId.Value == recipeVM.Id)
            {
                await updateRecipe();
                return;
            }

            lastRecipeId = recipeVM?.Id;

            recipeContainer.DOKill();
            emptyRecipeContainer.DOKill();

            recipeContainer.interactable = hasRecipe;
            emptyRecipeContainer.interactable = !hasRecipe;

            const float duration = 0.1f;

            await UniTask.WhenAll(
                recipeContainer.DOFade(0, duration).ToUniTask(),
                emptyRecipeContainer.DOFade(0, duration).ToUniTask());

            if (token.IsCancellationRequested)
            {
                return;
            }

            await updateRecipe();

            var showContainer = hasRecipe
                ? recipeContainer
                : emptyRecipeContainer;

            await showContainer.DOFade(1, duration);

            async UniTask updateRecipe()
            {
                if (hasRecipe)
                {
                    counterContainer.Init(recipeVM, recipeDisp);

                    await UniTask.WhenAll(
                        productContainer.Init(recipeVM, recipeDisp, token),
                        ingredientsContainer.Init(recipeVM, recipeDisp, token));

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    recipeNameText.SetTextParams(recipeVM.ProductVM.NameKey);

                    ingredientsContainer.IsAvailablle
                        .Subscribe(startCraftingButton.SetInteractableState)
                        .AddTo(recipeDisp);
                }
            }
        }

        private void StartCraftingCallback()
        {
            if (ingredientsContainer.IsAvailablle.Value)
            {
                var vendorVM = vendorsTabsContainer.VendorVM.Value;
                var recipeVM = vendorsTabsContainer.RecipeVM.Value;
                var count = counterContainer.Count.Value;

                var craftingEM = new StartCraftingEM
                {
                    VendorId = vendorVM.Id,
                    RecipeId = recipeVM.Id,
                    Count = count
                };

                craftVMF.StartCraftingProcess(craftingEM);

                counterContainer.ResetCount();
            }
        }
    }
}