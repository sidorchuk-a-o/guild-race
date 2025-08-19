using System.Collections.Generic;
using System.Linq;
using AD.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace Game.GuildLevels
{
    public class GuildLevelsService : Service, IGuildLevelsService
    {
        private readonly GuildLevelsState state;
        private readonly List<LevelContext> contexts = new();

        public IReadOnlyList<LevelInfo> Levels => state.Levels;

        public GuildLevelsService(GuildLevelsConfig config, IObjectResolver resolver)
        {
            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            return await Inited();
        }

        public void UnlockLevel(string levelId)
        {
            var level = Levels.FirstOrDefault(x => x.Id == levelId);
            var nextLevel = state.GetNextLevel(levelId);

            foreach (var context in contexts)
            {
                level.Mechanic.Apply(context);
            }

            level.MarkAsUnlocked();
            nextLevel?.MarkAsReadyUnlock();

            state.MarkAsDirty();
        }

        public void RegisterContext(LevelContext context)
        {
            contexts.Add(context);

            foreach (var level in Levels.Where(x => x.IsUnlocked.Value))
            {
                level.Mechanic.Apply(context);
            }
        }
    }
}