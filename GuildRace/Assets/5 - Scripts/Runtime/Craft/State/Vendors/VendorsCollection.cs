using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class VendorsCollection : ReactiveCollection<VendorInfo>, IVendorsCollection
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