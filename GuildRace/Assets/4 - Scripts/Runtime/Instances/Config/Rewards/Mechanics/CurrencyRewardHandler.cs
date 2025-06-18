using System.Collections.Generic;
using System.Linq;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class CurrencyRewardHandler : RewardHandler
    {
        [SerializeField] private float failedMod = 0.3f;

        private IStoreService storeService;

        [Inject]
        public void Inject(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        public CurrencyAmount GetCurrencyAmount(InstanceRewardData reward)
        {
            var currencyKey = new CurrencyKey(reward.MechanicParams[0]);
            var currencyValue = reward.MechanicParams[1].IntParse();

            return new CurrencyAmount(currencyKey, currencyValue);
        }

        public override IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result)
        {
            return rewards.Select(x => ApplyReward(x, result));
        }

        public override RewardResult ApplyReward(InstanceRewardData reward, CompleteResult result)
        {
            var amount = GetCurrencyAmount(reward);

            if (result != CompleteResult.Completed)
            {
                amount *= failedMod;
            }

            storeService.CurrenciesModule.AddCurrency(amount);

            return new CurrencyRewardResult
            {
                Amount = amount,
                RewardId = reward.Id
            };
        }
    }
}