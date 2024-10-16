using AD.Services.Router;
using AD.ToolsCollection;
using System.Collections.Generic;
using UniRx;

namespace Game.Guild
{
    public class RecruitingVM : VMBase
    {
        private readonly IRecruitingModule module;

        public IReadOnlyReactiveProperty<bool> IsEnabled { get; }
        public IReadOnlyList<ClassRoleSelectorVM> ClassRoleSelectorsVM { get; }

        public RecruitingVM(IRecruitingModule module, GuildVMFactory guildVMF)
        {
            this.module = module;

            IsEnabled = module.IsEnabled;
            ClassRoleSelectorsVM = guildVMF.GetClassRoleSelectors();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            foreach (var classRoleSelectorVM in ClassRoleSelectorsVM)
            {
                classRoleSelectorVM.AddTo(disp);
            }
        }

        public void SwitchRecruitingState()
        {
            module.SwitchRecruitingState();
        }
    }
}