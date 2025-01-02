using AD.Services.Localization;
using AD.Services.Router;
using Game.Guild;

namespace Game.Instances
{
    public class RoleTabVM : ViewModel
    {
        public RoleId Role { get; }
        public LocalizeKey NameKey { get; }

        public CharactersVM CharactersVM { get; }

        public RoleTabVM(RoleData data, CharactersVM characters)
        {
            Role = data.Id;
            NameKey = data.NameKey;
            CharactersVM = characters;
        }

        protected override void InitSubscribes()
        {
            CharactersVM.AddTo(this);
        }
    }
}