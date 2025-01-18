using AD.Services.Router;
using UnityEngine;

namespace Game.Craft
{
    public class RecipesScrollView : VMScrollView<RecipeVM>
    {
        [SerializeField] private RectTransform recipeItemPrefab;

        protected override VMItemHolder CreateItemHolder(RecipeVM viewModel)
        {
            return new VMItemHolder<RecipeVM>();
        }

        protected override RectTransform GetItemPrefab(RecipeVM viewModel)
        {
            return recipeItemPrefab;
        }
    }
}