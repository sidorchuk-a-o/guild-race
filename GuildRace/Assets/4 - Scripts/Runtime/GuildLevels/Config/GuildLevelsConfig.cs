using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.GuildLevels
{
    public class GuildLevelsConfig : ScriptableConfig
    {
        [SerializeField] private List<LevelData> levels;

        public IReadOnlyList<LevelData> Levels => levels;
    }
}