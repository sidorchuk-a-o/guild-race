using System.Collections.Generic;
using AD.Services.Router;
using Game.Craft;

namespace Game.Store
{
    public class ReagentRewardsVM : VMCollection<ReagentRewardData, ReagentRewardVM>
    {
        private readonly CraftVMFactory craftVMF;

        public ReagentRewardsVM(IReadOnlyCollection<ReagentRewardData> values, CraftVMFactory craftVMF) : base(values)
        {
            this.craftVMF = craftVMF;
        }

        protected override ReagentRewardVM Create(ReagentRewardData value)
        {
            return new ReagentRewardVM(value, craftVMF);
        }
    }
}