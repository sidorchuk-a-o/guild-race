using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Craft
{
    public class RecipeVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }

        public RecipeVM(RecipeData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}