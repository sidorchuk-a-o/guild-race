using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class RewardsParams
    {
        [SerializeField] private List<InstanceRewardData> rewards;
        [SerializeField] private List<RewardHandler> rewardHandlers;

        public List<InstanceRewardData> Rewards => rewards;
        public List<RewardHandler> RewardHandlers => rewardHandlers;
    }
}