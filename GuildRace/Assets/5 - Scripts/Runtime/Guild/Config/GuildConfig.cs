using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Guild
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

        public EmblemEM DefaultEmblemEM { get; } = new()
        {
            Symbol = 8,
            Background = 1,
            SymbolColor = 1,
            BackgroundColors = new() { 3, 5, 10, 15, 20 }
        };
    }
}