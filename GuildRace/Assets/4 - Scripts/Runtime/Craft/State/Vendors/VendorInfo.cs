using AD.Services.Localization;

namespace Game.Craft
{
    public class VendorInfo
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public IRecipesCollection Recipes { get; }

        public VendorInfo(VendorData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            Recipes = new RecipesCollection(data.Recipes);
        }
    }
}