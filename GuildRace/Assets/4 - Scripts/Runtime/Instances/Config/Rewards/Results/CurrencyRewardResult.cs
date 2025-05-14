using AD.Services.Store;

namespace Game.Instances
{
    public class CurrencyRewardResult : RewardResult
    {
        public CurrencyKey CurrencyKey { get; set; }
        public int CurrencyValue { get; set; }
    }
}