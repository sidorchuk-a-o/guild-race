using System.Collections.Generic;
using AD.Services.Store;
using UnityEngine;

namespace Game.Store
{
    public class ReagentsReward : RewardData
    {
        [Header("Reagents")]
        [SerializeField] private List<ReagentRewardData> reagentRewards;

        public IReadOnlyList<ReagentRewardData> ReagentRewards => reagentRewards;
    }
}