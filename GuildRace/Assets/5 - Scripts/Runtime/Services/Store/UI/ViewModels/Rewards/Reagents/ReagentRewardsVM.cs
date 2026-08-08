using System.Collections.Generic;
using AD.Game.Craft;
using AD.Router;

namespace AD.Game.Store
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
            return new(value, craftVMF);
        }
    }
}