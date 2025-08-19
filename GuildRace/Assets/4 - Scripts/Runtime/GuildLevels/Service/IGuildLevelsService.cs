using System.Collections.Generic;

namespace Game.GuildLevels
{
    public interface IGuildLevelsService
    {
        IReadOnlyList<LevelInfo> Levels { get; }

        void UnlockLevel(string levelId);
        void RegisterContext(LevelContext context);
    }
}