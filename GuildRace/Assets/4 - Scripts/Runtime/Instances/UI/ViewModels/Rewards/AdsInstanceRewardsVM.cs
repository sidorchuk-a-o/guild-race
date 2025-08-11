using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class AdsInstanceRewardsVM : VMCollection<AdsInstanceRewardInfo, AdsInstanceRewardVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public AdsInstanceRewardsVM(IReadOnlyCollection<AdsInstanceRewardInfo> values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override AdsInstanceRewardVM Create(AdsInstanceRewardInfo value)
        {
            return new AdsInstanceRewardVM(value, instancesVMF);
        }
    }
}