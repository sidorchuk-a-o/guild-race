using Game.GuildLevels;
using UniRx;

namespace Game.Guild
{
    public class GuildLevelContext : LevelContext
    {
        private readonly ReactiveProperty<int> bankRowCount = new();
        private readonly ReactiveProperty<int> charactersCount = new();
        private readonly ReactiveProperty<float> requestTimePercent = new(1);

        public IReadOnlyReactiveProperty<int> BankRowCount => bankRowCount;
        public IReadOnlyReactiveProperty<int> CharactersCount => charactersCount;
        public IReadOnlyReactiveProperty<float> RequestTimePercent => requestTimePercent;

        public void AddBankRowCount(int count)
        {
            bankRowCount.Value += count;
        }

        public void AddCharactersCount(int count)
        {
            charactersCount.Value += count;
        }

        public void AddRequestTimePercent(float mod)
        {
            requestTimePercent.Value -= mod;
        }
    }
}