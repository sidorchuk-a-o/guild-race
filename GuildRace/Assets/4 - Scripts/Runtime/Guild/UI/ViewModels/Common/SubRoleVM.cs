using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Guild
{
    public class SubRoleVM : ViewModel
    {
        public SubRoleId Id { get; }
        public LocalizeKey NameKey { get; }

        public SubRoleVM(SubRoleData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}