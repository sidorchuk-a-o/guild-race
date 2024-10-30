using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Guild
{
    public class RoleVM : ViewModel
    {
        public RoleId Id { get; }
        public LocalizeKey NameKey { get; }

        public RoleVM(RoleData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}