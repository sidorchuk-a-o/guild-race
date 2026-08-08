using System;
using System.Collections.Generic;
using System.Linq;
using AD.Leaderboards;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class LeaderboardParams
    {
        [SerializeField] private LeaderboardKey guildScoreKey;
        [SerializeField] private List<InstanceScoreData> instanceScores;

        public LeaderboardKey GuildScoreKey => guildScoreKey;
        public IReadOnlyList<InstanceScoreData> InstanceScores => instanceScores;

        public InstanceScoreData GetScoreParams(InstanceType instanceType)
        {
            return instanceScores.FirstOrDefault(x => x.Type == instanceType);
        }
    }
}