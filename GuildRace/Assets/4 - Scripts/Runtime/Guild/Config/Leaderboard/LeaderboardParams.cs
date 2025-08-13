using AD.Services.Leaderboards;
using System;
using UnityEngine;

namespace Game.Guild
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