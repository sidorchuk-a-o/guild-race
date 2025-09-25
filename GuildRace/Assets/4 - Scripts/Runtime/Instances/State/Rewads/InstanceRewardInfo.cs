namespace Game.Instances
{
    public class InstanceRewardInfo
    {
        public InstanceRewardData Data { get; }
        public RewardResult Result { get; }

        public InstanceRewardInfo(InstanceRewardData data, RewardResult result = null)
        {
            Data = data;
            Result = result;
        }
    }
}