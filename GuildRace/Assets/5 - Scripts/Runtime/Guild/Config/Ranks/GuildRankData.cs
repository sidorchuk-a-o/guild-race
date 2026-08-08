using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Guild
{
    public class GuildRankData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey defaultNameKey;

        public LocalizeKey DefaultNameKey => defaultNameKey;
    }
}