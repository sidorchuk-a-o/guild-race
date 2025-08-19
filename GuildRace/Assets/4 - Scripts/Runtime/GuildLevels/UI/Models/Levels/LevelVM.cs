using AD.Services.Router;
using AD.Services.Store;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.GuildLevels
{
    public class LevelVM : ViewModel
    {
        public string Id { get; }
        public int Level { get; }

        public UITextData DescData { get; }
        public CurrencyVM UnlockPriceVM { get; }

        public IReadOnlyReactiveProperty<bool> IsUnlocked { get; }
        public IReadOnlyReactiveProperty<bool> ReadyToUnlock { get; }

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