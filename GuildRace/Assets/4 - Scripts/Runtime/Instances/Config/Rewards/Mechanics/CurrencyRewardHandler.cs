using AD.Services.Store;
using AD.ToolsCollection;
using VContainer;

namespace Game.Instances
{
    public class CurrencyRewardHandler : RewardHandler
    {
        private IStoreService storeService;

        [Inject]
        public void Inject(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        public override void ApplyReward(InstanceRewardData reward)
        {
            var currencyKey = new CurrencyKey(reward.MechanicParams[0]);
            var currencyValue = reward.MechanicParams[1].IntParse();

            var amount = new CurrencyAmount(currencyKey, currencyValue);

            storeService.CurrenciesModule.AddCurrency(amount);
        }
    }
}