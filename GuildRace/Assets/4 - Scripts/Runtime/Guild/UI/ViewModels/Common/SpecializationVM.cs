using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;

namespace Game.Guild
{
    public class SpecializationVM : VMBase
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

        protected override void InitSubscribes(CompositeDisp disp)
        {
            RoleVM.AddTo(disp);
        }
    }
}