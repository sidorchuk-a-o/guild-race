using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public interface IVendorsCollection : IReadOnlyReactiveCollection<VendorInfo>
    {
        RecipeData GetRecipe(int recipeId);
    }
}