using System;
using System.Linq;
using System.Collections.Generic;
using AD.Services.Leaderboards;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class LeaderboardParams
    {
        [SerializeField] private LeaderboardKey guildScoreKey;
        [SerializeField] private float sendTime;
        [SerializeField] private List<InstanceScoreData> instanceScores;

        public LeaderboardKey GuildPowerKey => guildScoreKey;
        public float SendTime => sendTime;
        public IReadOnlyList<InstanceScoreData> InstanceScores => instanceScores;

        public InstanceScoreData GetScoreParams(InstanceType instanceType)
        {
            return instanceScores.FirstOrDefault(x => x.Type == instanceType);
        }
    }
}