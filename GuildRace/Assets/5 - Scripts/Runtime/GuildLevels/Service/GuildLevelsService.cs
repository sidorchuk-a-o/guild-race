using System.Collections.Generic;
using System.Linq;
using AD.Analytics;
using AD.Management;
using Cysharp.Threading.Tasks;

namespace AD.Game.GuildLevels
{
    public class GuildLevelsService : Service, IGuildLevelsService
    {
        private readonly GuildLevelsState state;
        private readonly IAnalyticsService analytics;

        private readonly List<LevelContext> contexts = new();

        public IReadOnlyList<LevelInfo> Levels => state.Levels;

        public GuildLevelsService(GuildLevelsState state, IAnalyticsService analytics)
        {
            this.state = state;
            this.analytics = analytics;
        }

        public override UniTask<bool> Init()
        {
            return Inited();
        }

        public void UnlockLevel(string levelId)
        {
            var level = Levels.First(x => x.Id == levelId);
            var nextLevel = state.GetNextLevel(levelId);

            foreach (var context in contexts)
            {
                level.Mechanic.Apply(context);
            }

            level.MarkAsUnlocked();
            nextLevel?.MarkAsReadyUnlock();

            state.MarkAsDirty();

            analytics.UnlockLevel(level);
        }

        public void RegisterContext(LevelContext context)
        {
            contexts.Add(context);

            foreach (var level in Levels.Where(x => x.IsUnlocked.CurrentValue))
            {
                level.Mechanic.Apply(context);
            }
        }
    }
}