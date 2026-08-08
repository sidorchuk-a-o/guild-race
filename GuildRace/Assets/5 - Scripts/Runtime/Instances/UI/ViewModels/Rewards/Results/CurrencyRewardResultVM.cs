using AD.Router;
using AD.Store;

namespace AD.Game.Instances
{
    public class CurrencyRewardResultVM : RewardResultVM
    {
        public CurrencyVM CurrencyVM { get; }

        public CurrencyRewardResultVM(CurrencyRewardResult info, InstancesVMFactory instancesVMF) : base(info, instancesVMF)
        {
            CurrencyVM = instancesVMF.StoreVMF.GetCurrency(info.Amount);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            CurrencyVM.AddTo(this);
        }
    }
}