using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;

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

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }
    }
}