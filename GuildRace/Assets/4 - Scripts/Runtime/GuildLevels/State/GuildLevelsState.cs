using System.Collections.Generic;
using System.Linq;
using AD.Services.Save;
using AD.States;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.GuildLevels
{
    public class GuildLevelsState : State<GuildLevelsSM>
    {
        private readonly GuildLevelsConfig config;
        private readonly IObjectResolver resolver;

        private readonly List<LevelInfo> levels = new();

        public override string SaveKey => GuildLevelsSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IReadOnlyList<LevelInfo> Levels => levels;

        public GuildLevelsState(GuildLevelsConfig config, IObjectResolver resolver) : base(resolver)
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
            return new GuildLevelsSM()
            {
                GuildLevels = Levels
            };
        }

        protected override void ReadSave(GuildLevelsSM save)
        {
            if (save == null)
            {
                levels.AddRange(CreateDefaultGuildLevels());

                return;
            }

            levels.AddRange(save.GetGuildLevels(config, resolver));

            UpdateLevels();
        }

        private void UpdateLevels()
        {
            var lastReadyIndex = levels.FindLastIndex(x => x.ReadyToUnlock.Value);
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