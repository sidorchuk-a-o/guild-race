using System.Collections.Generic;
using AD.ToolsCollection;

namespace Game.Instances
{
    public abstract class RewardHandler : ScriptableEntity<int>
    {
        public abstract void ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result);
        public abstract void ApplyReward(InstanceRewardData reward, CompleteResult result);
    }
}