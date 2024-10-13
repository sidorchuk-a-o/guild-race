using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    public class GuildConfig : ScriptableConfig
    {
        [SerializeField] private int maxCharactersCount;
        [SerializeField] private List<GuildRankData> defaultGuildRanks;

        [SerializeField] private CharactersModuleData charactersModule = new();
        [SerializeField] private RecruitingModuleData recruitingModule = new();

        public int MaxCharactersCount => maxCharactersCount;
        public IReadOnlyList<GuildRankData> DefaultGuildRanks => defaultGuildRanks;

        public CharactersModuleData CharactersModule => charactersModule;
        public RecruitingModuleData RecruitingModule => recruitingModule;
    }
}