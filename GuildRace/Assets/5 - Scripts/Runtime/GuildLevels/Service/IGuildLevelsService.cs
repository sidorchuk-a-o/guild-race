using System.Collections.Generic;

namespace AD.Game.GuildLevels
{
    public interface IGuildLevelsService
    {
        IReadOnlyList<LevelInfo> Levels { get; }

        void UnlockLevel(string levelId);
        void RegisterContext(LevelContext context);
    }
}