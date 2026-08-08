using System.Collections.Generic;
using System.Linq;
using AD.Management;
using AD.Save;
using UnityEngine;
using VContainer;

namespace AD.Game.GuildLevels
{
    public class GuildLevelsState : ServiceState<GuildLevelsSM>
    {
        private readonly GuildLevelsConfig config;
        private readonly IObjectResolver resolver;

        private readonly List<LevelInfo> levels = new();

        public override string SaveKey => "guild-levels";
        public override SaveSource SaveSource => SaveSource.App;

        public IReadOnlyList<LevelInfo> Levels => levels;

        public GuildLevelsState(GuildLevelsConfig config, IObjectResolver resolver)
        {
            this.config = config;
            this.resolver = resolver;
        }

        public LevelInfo GetNextLevel(string levelId)
        {
            var levelIndex = levels.FindIndex(x => x.Id == levelId);

            return levels.ElementAtOrDefault(levelIndex + 1);
        }

        // == Save ==

        protected override GuildLevelsSM CreateSave()
        {
            return new()
            {
                GuildLevels = Levels
            };
        }

        protected override void CreateDefaultState()
        {
            levels.AddRange(CreateDefaultGuildLevels());
        }

        protected override void ReadSave(GuildLevelsSM save)
        {
            levels.AddRange(save.GetGuildLevels(config, resolver));

            UpdateLevels();
        }

        private void UpdateLevels()
        {
            var lastReadyIndex = levels.FindLastIndex(x => x.ReadyToUnlock.CurrentValue);
            var lastIndex = Mathf.Max(0, lastReadyIndex - 1);

            for (var i = lastIndex; i >= 0; i--)
            {
                levels[i].MarkAsReadyUnlock();
            }
        }

        private IEnumerable<LevelInfo> CreateDefaultGuildLevels()
        {
            return config.Levels.Select((x, i) =>
            {
                resolver.Inject(x.Mechanic);

                var level = new LevelInfo(level: i + 1, data: x);

                if (i == 0)
                {
                    level.MarkAsReadyUnlock();
                }

                return level;
            });
        }
    }
}