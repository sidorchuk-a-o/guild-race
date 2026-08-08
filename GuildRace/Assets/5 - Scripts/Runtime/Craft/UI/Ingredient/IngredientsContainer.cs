using System.Collections.Generic;
using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Craft
{
    public class IngredientsContainer : MonoBehaviour
    {
        [SerializeField] private List<IngredientItemSlot> ingredientItems;

        private RecipeVM recipeVM;

        public async UniTask Init(RecipeVM recipeVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.recipeVM = recipeVM;

            await InitIngredientItems(disp, ct);
        }

        private async UniTask InitIngredientItems(CompositeDisp disp, CancellationTokenSource ct)
        {
            var itemsCount = ingredientItems.Count;
            var ingredientsCount = recipeVM.IngredientsVM.Count;

            // update active state
            for (var i = 0; i < itemsCount; i++)
            {
                var isActive = i < ingredientsCount;
                var item = ingredientItems[i];

                item.SetActive(isActive);
            }

            // init tasks
            var initItemsTasks = recipeVM.IngredientsVM.Select((ingridientVM, i) =>
            {
                var item = ingredientItems[i];

                return item.Init(ingridientVM, disp, ct);
            });

            await UniTask.WhenAll(initItemsTasks);
        }
    }
}