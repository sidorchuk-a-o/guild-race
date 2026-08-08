using AD.Router;

namespace AD.Game.Craft
{
    public class RecipesVM : VMCollection<RecipeData, RecipeVM>
    {
        private readonly CraftVMFactory craftVMF;

        public RecipesVM(IRecipesCollection values, CraftVMFactory craftVMF) : base(values)
        {
            this.craftVMF = craftVMF;
        }

        protected override RecipeVM Create(RecipeData value)
        {
            return new(value, craftVMF);
        }
    }
}