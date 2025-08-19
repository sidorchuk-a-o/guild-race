using System.Collections.Generic;

namespace Game.Craft
{
    public interface IVendorsCollection : IReadOnlyCollection<VendorInfo>
    {
        RecipeData GetRecipe(int recipeId);
    }
}