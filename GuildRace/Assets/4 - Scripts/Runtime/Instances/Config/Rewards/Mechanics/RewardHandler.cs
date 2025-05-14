using System.Collections.Generic;
using AD.ToolsCollection;

namespace Game.Instances
{
    public abstract class RewardHandler : ScriptableEntity<int>
    {
        public abstract IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result);
        public abstract RewardResult ApplyReward(InstanceRewardData reward, CompleteResult result);
    }
}