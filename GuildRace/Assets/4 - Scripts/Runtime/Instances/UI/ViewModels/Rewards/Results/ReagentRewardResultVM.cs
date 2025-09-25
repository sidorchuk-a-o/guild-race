namespace Game.Instances
{
    public class ReagentRewardResultVM : RewardResultVM
    {
        public int Count { get; }

        public ReagentRewardResultVM(ReagentRewardResult info, InstancesVMFactory instancesVMF) 
            : base(info, instancesVMF)
        {
            Count = info.Count;
        }
    }
}