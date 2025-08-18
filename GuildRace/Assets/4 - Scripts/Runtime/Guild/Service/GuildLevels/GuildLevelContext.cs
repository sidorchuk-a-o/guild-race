using Game.GuildLevels;
using UniRx;

namespace Game.Guild
{
    public class GuildLevelContext : LevelContext
    {
        private readonly ReactiveProperty<int> charactersCount = new();

        public IReadOnlyReactiveProperty<int> CharactersCount => charactersCount;

        public void AddCharactersCount(int value)
        {
            charactersCount.Value += value;
        }
    }
}