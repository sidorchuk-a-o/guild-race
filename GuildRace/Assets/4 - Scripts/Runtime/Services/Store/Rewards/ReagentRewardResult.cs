using System.Collections.Generic;
using AD.Services.Store;

namespace Game.Store
{
    public class ReagentRewardResult : RewardResult
    {
        public List<string> ItemIds { get; set; }
        public int ItemDataId { get; set; }
        public int Count { get; set; }
    }
}