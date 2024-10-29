using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class ClassRoleSelectorVM : ViewModel
    {
        private readonly ClassRoleSelectorInfo info;
        private readonly GuildVMFactory guildVMF;

        public RoleVM RoleVM { get; }

        public ClassRoleSelectorVM(ClassRoleSelectorInfo info, GuildVMFactory guildVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;

            RoleVM = guildVMF.GetRole(info.RoleId);
        }

        protected override void InitSubscribes()
        {
            RoleVM.AddTo(this);

            info.IsEnabled
                .Subscribe(SetSelectState)
                .AddTo(this);
        }

        public void SwitchSelectState()
        {
            guildVMF.SetClassRoleSelectorState(info.RoleId, !info.IsEnabled.Value);
        }
    }
}