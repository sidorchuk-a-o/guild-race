using AD.Router;
using AD.Store;
using AD.UI;
using R3;

namespace AD.Game.GuildLevels
{
    public class LevelVM : ViewModel
    {
        public string Id { get; }
        public int Level { get; }

        public UITextData DescData { get; }
        public CurrencyVM UnlockPriceVM { get; }

        public ReadOnlyReactiveProperty<bool> IsUnlocked { get; }
        public ReadOnlyReactiveProperty<bool> ReadyToUnlock { get; }

        public LevelVM(LevelInfo info, GuildLevelsVMFactory levelsVMF)
        {
            Id = info.Id;
            Level = info.Level;
            DescData = info.DescData;
            IsUnlocked = info.IsUnlocked;
            ReadyToUnlock = info.ReadyToUnlock;
            UnlockPriceVM = levelsVMF.StoreVMF.GetCurrency(info.UnlockPrice);
        }

        protected override void InitSubscribes()
        {
            UnlockPriceVM.AddTo(this);
        }
    }
}