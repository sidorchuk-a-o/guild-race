using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Game.Craft
{
    public class IngredientsContainer : MonoBehaviour
    {
        [SerializeField] private CraftingCounterContainer counterContainer;
        [SerializeField] private List<IngredientItemSlot> ingredientItems;

        private readonly ReactiveProperty<bool> isAvailablle = new();

        private RecipeVM recipeVM;

        public IReadOnlyReactiveProperty<bool> IsAvailablle => isAvailablle;

        private void Awake()
        {
            foreach (var ingridientVM in ingredientItems)
            {
                ingridientVM.IsAvailable
                    .SilentSubscribe(UpdateAvailableState)
                    .AddTo(this);
            }
        }

        public async UniTask Init(RecipeVM recipeVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.recipeVM = recipeVM;

            await InitIngredientItems(disp, ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            counterContainer.Count
                .Subscribe(CounterChangedCallback)
                .AddTo(disp);
        }

        private async UniTask InitIngredientItems(CompositeDisp disp, CancellationTokenSource ct)
        {
            var itemsCount = ingredientItems.Count;
            var ingridientsCount = recipeVM.IngridientsVM.Count;
            var craftingCount = counterContainer.Count.Value;

            // update active state
            for (var i = 0; i < itemsCount; i++)
            {
                var isActive = i < ingridientsCount;
                var item = ingredientItems[i];

                item.SetActive(isActive);
            }

            // init tasks
            var initItemsTasks = recipeVM.IngridientsVM.Select((ingridientVM, i) =>
            {
                var item = ingredientItems[i];

                return item.Init(ingridientVM, craftingCount, disp, ct);
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

            UpdateAvailableState();
        }

        private void UpdateAvailableState()
        {
            var hasCraft = counterContainer.Count.Value > 0;

            var ingredientsAvailable = ingredientItems
                .Where(x => x.isActiveAndEnabled)
                .All(x => x.IsAvailable.Value);

            isAvailablle.Value = ingredientsAvailable && hasCraft;
        }
    }
}