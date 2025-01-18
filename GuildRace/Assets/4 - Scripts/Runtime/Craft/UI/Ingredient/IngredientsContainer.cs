using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Craft
{
    public class IngredientsContainer : MonoBehaviour
    {
        [SerializeField] private CraftingCounterContainer counterContainer;
        [SerializeField] private List<IngredientItem> ingredientItems;

        private readonly ReactiveProperty<bool> isAvailablle = new();

        private CraftVMFactory craftVMF;
        private IReadOnlyList<IngredientVM> ingridientsVM;

        public IReadOnlyReactiveProperty<bool> IsAvailablle => isAvailablle;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        private void Awake()
        {
            foreach (var item in ingredientItems)
            {
                item.IsAvailable
                    .SilentSubscribe(UpdateAvailableState)
                    .AddTo(this);
            }
        }

        public async UniTask Init(RecipeVM recipeVM, CancellationTokenSource token, CompositeDisp disp)
        {
            ingridientsVM = craftVMF.GetRecipeIngredients(recipeVM.Id);
            ingridientsVM.ForEach(x => x.AddTo(disp));

            await InitIngredientItems(token, disp);

            if (token.IsCancellationRequested)
            {
                return;
            }

            counterContainer.Count
                .Subscribe(CounterChangedCallback)
                .AddTo(disp);
        }

        private async UniTask InitIngredientItems(CancellationTokenSource token, CompositeDisp disp)
        {
            var itemsCount = ingredientItems.Count;
            var ingridientsCount = ingridientsVM.Count;
            var craftingCount = counterContainer.Count.Value;

            // update active state
            for (var i = 0; i < itemsCount; i++)
            {
                var isActive = i < ingridientsCount;
                var item = ingredientItems[i];

                item.SetActive(isActive);
            }

            // init tasks
            var initItemsTasks = ingridientsVM.Select((ingridientVM, i) =>
            {
                var item = ingredientItems[i];

                return item.Init(ingridientVM, craftingCount, token, disp);
            });

            await UniTask.WhenAll(initItemsTasks);
        }

        private void CounterChangedCallback(int count)
        {
            var activeItems = ingredientItems.Where(x => x.isActiveAndEnabled);

            foreach (var ingredientItem in activeItems)
            {
                ingredientItem.SetCraftingCount(count);
            }
        }

        private void UpdateAvailableState()
        {
            isAvailablle.Value = ingredientItems
                .Where(x => x.isActiveAndEnabled)
                .All(x => x.IsAvailable.Value);
        }
    }
}