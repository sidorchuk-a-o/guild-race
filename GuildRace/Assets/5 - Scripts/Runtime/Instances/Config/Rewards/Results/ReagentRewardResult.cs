using System.Collections.Generic;

namespace AD.Game.Instances
{
    public class ReagentRewardResult : RewardResult
    {
        public List<string> ItemIds { get; set; }
        public int ItemDataId { get; set; }
        public int Count { get; set; }
    }
}