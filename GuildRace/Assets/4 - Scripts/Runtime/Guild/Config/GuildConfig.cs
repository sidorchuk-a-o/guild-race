using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    public class GuildConfig : ScriptableConfig
    {
        [SerializeField] private int maxCharactersCount;
        [SerializeField] private List<GuildRankData> defaultGuildRanks;

        [SerializeField] private CharactersParams charactersParams;
        [SerializeField] private RecruitingParams recruitingParams;
        [SerializeField] private GuildBankParams guildBankParams;
        [SerializeField] private EmblemParams emblemParams;
        [SerializeField] private LeaderboardParams leaderboardParams;

        public int MaxCharactersCount => maxCharactersCount;
        public IReadOnlyList<GuildRankData> DefaultGuildRanks => defaultGuildRanks;

        public CharactersParams CharactersParams => charactersParams;
        public RecruitingParams RecruitingParams => recruitingParams;
        public GuildBankParams GuildBankParams => guildBankParams;
        public EmblemParams EmblemParams => emblemParams;
        public LeaderboardParams LeaderboardParams => leaderboardParams;
    }
}