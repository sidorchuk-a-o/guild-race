using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Guild
{
    public class SpecializationVM : ViewModel
    {
        public SpecializationId Id { get; }
        public LocalizeKey NameKey { get; }

        public RoleVM RoleVM { get; }

        public SpecializationVM(SpecializationData data, GuildVMFactory guildVMF)
        {
            Id = data.Id;
            NameKey = data.NameKey;

            RoleVM = guildVMF.GetRole(data.RoleId);
        }

        protected override void InitSubscribes()
        {
            RoleVM.AddTo(this);
        }
    }
}