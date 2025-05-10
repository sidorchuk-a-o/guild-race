using AD.ToolsCollection;

namespace Game.Instances
{
    public abstract class RewardHandler : ScriptableEntity<int>
    {
        public abstract void ApplyReward(InstanceRewardData reward);
    }
}