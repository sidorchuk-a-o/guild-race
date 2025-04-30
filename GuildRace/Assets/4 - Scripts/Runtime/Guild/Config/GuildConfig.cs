using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    public class GuildConfig : ScriptableConfig
    {
        [SerializeField] private int maxCharactersCount;
        [SerializeField] private List<GuildRankData> defaultGuildRanks;

        [SerializeField] private CharactersParams charactersParams = new();
        [SerializeField] private RecruitingParams recruitingParams = new();
        [SerializeField] private GuildBankParams guildBankParams = new();
        [SerializeField] private EmblemParams emblemParams = new();

        public int MaxCharactersCount => maxCharactersCount;
        public IReadOnlyList<GuildRankData> DefaultGuildRanks => defaultGuildRanks;

        public CharactersParams CharactersParams => charactersParams;
        public RecruitingParams RecruitingParams => recruitingParams;
        public GuildBankParams GuildBankParams => guildBankParams;
        public EmblemParams EmblemParams => emblemParams;
    }
}