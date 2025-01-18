using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Craft
{
    public class VendorVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public RecipesVM RecipesVM { get; }

        public VendorVM(VendorInfo info)
        {
            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            RecipesVM = new(info.Recipes);
        }

        protected override void InitSubscribes()
        {
            RecipesVM.AddTo(this);
        }
    }
}