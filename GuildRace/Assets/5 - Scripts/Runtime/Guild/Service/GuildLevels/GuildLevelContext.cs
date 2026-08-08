using AD.Game.GuildLevels;
using R3;

namespace AD.Game.Guild
{
    public class GuildLevelContext : LevelContext
    {
        private readonly ReactiveProperty<int> bankRowCount = new();
        private readonly ReactiveProperty<int> charactersCount = new();
        private readonly ReactiveProperty<float> requestTimePercent = new(1);

        public ReadOnlyReactiveProperty<int> BankRowCount => bankRowCount;
        public ReadOnlyReactiveProperty<int> CharactersCount => charactersCount;
        public ReadOnlyReactiveProperty<float> RequestTimePercent => requestTimePercent;

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