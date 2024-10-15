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
        public IReadOnlyList<ClassWeightVM> ClassWeightsVM { get; }

        public RecruitingVM(IRecruitingModule module, GuildVMFactory guildVMF)
        {
            this.module = module;

            IsEnabled = module.IsEnabled;
            ClassWeightsVM = guildVMF.GetClassWeights();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            foreach (var classWeightVM in ClassWeightsVM)
            {
                classWeightVM.AddTo(disp);
            }
        }

        public void SwitchRecruitingState()
        {
            module.SwitchRecruitingState();
        }
    }
}