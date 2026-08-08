using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class RecipesCollection : ReactiveCollection<RecipeData>, IRecipesCollection
    {
        public RecipesCollection(IEnumerable<RecipeData> values) : base(values)
        {
        }
    }
}