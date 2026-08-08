using AD.Router;
using AD.Store;

namespace AD.Game.Instances
{
    public class CurrencyRewardMechanicVM : RewardMechanicVM
    {
        public CurrencyVM CurrencyVM { get; }

        public CurrencyRewardMechanicVM(InstanceRewardData data, CurrencyRewardHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            var currencyAmount = CurrencyRewardHandler.GetCurrencyAmount(data);

            CurrencyVM = instancesVMF.StoreVMF.GetCurrency(currencyAmount);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            CurrencyVM.AddTo(this);
        }
    }
}