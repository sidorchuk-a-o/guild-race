using AD.Services.Router;
using System.Collections.Generic;
using UniRx;

namespace Game.Guild
{
    public class RecruitingVM : ViewModel
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

        protected override void InitSubscribes()
        {
            foreach (var classRoleSelectorVM in ClassRoleSelectorsVM)
            {
                classRoleSelectorVM.AddTo(this);
            }
        }

        public void SwitchRecruitingState()
        {
            module.SwitchRecruitingState();
        }
    }
}