using AD.Services.Store;
using AD.UI;
using UniRx;

namespace Game.GuildLevels
{
    public class LevelInfo
    {
        private readonly ReactiveProperty<bool> isUnlocked = new();
        private readonly ReactiveProperty<bool> readyToUnlock = new();

        public string Id { get; }
        public int Level { get; }

        public UITextData DescData { get; }
        public LevelMechanic Mechanic { get; }

        public CurrencyAmount UnlockPrice { get; }
        public IReadOnlyReactiveProperty<bool> IsUnlocked => isUnlocked;
        public IReadOnlyReactiveProperty<bool> ReadyToUnlock => readyToUnlock;

        public LevelInfo(int level, LevelData data)
        {
            Id = data.Id;
            Level = level;
            DescData = data.Mechanic.GetDesc(data.DescKey);
            Mechanic = data.Mechanic;
            UnlockPrice = data.UnlockPrice;
        }

        public void MarkAsReadyUnlock()
        {
            readyToUnlock.Value = true;
        }

        public void MarkAsUnlocked()
        {
            isUnlocked.Value = true;
        }
    }
}