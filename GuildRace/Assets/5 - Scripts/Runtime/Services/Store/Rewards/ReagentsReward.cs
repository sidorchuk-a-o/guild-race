using System.Collections.Generic;
using AD.Store;
using UnityEngine;

namespace AD.Game.Store
{
    public class ReagentsReward : RewardData
    {
        [Header("Reagents")]
        [SerializeField] private List<ReagentRewardData> reagentRewards;

        public IReadOnlyList<ReagentRewardData> ReagentRewards => reagentRewards;
    }
}