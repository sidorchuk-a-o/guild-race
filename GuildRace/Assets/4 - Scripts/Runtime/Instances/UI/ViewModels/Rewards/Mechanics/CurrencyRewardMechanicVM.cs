using AD.Services.Router;
using AD.Services.Store;

namespace Game.Instances
{
    public class CurrencyRewardMechanicVM : RewardMechanicVM
    {
        public CurrencyVM CurrencyVM { get; }

        public CurrencyRewardMechanicVM(InstanceRewardData data, CurrencyRewardHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            var currencyAmount = handler.GetCurrencyAmount(data);

            CurrencyVM = instancesVMF.StoreVMF.GetCurrency(currencyAmount);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            CurrencyVM.AddTo(this);
        }
    }
}