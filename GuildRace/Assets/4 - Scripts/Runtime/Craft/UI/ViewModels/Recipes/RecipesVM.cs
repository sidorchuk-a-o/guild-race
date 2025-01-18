using AD.Services.Router;

namespace Game.Craft
{
    public class RecipesVM : VMCollection<RecipeData, RecipeVM>
    {
        public RecipesVM(IRecipesCollection values) : base(values)
        {
        }

        protected override RecipeVM Create(RecipeData value)
        {
            return new RecipeVM(value);
        }
    }
}