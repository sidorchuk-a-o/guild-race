using AD.States;
using System.Linq;
using System.Collections.Generic;

namespace Game.Craft
{
    public class VendorsCollection : ReactiveCollectionInfo<VendorInfo>, IVendorsCollection
    {
        public VendorsCollection(IEnumerable<VendorInfo> values) : base(values)
        {
        }

        public RecipeData GetRecipe(int recipeId)
        {
            return Values
                .SelectMany(x => x.Recipes)
                .FirstOrDefault(x => x.Id == recipeId);
        }
    }
}