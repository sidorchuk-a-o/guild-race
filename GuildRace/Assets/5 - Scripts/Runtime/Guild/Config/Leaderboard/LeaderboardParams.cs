using System;
using AD.Leaderboards;
using UnityEngine;

namespace AD.Game.Guild
{
    [Serializable]
    public class LeaderboardParams
    {
        [SerializeField] private LeaderboardKey guildPowerKey;
        [SerializeField] private float sendTime;

        public LeaderboardKey GuildPowerKey => guildPowerKey;
        public float SendTime => sendTime;
    }
}