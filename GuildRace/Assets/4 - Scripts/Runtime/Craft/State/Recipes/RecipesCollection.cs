using AD.States;
using System.Collections.Generic;

namespace Game.Craft
{
    public class RecipesCollection : ReactiveCollectionInfo<RecipeData>, IRecipesCollection
    {
        public RecipesCollection(IEnumerable<RecipeData> values) : base(values)
        {
        }
    }
}