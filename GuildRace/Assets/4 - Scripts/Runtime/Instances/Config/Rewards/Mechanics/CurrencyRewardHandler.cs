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

        public override IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result)
        {
            return rewards.Select(x => ApplyReward(x, result));
        }

        public override RewardResult ApplyReward(InstanceRewardData reward, CompleteResult result)
        {
            var currencyKey = new CurrencyKey(reward.MechanicParams[0]);
            var currencyValue = reward.MechanicParams[1].IntParse();

            if (result != CompleteResult.Completed)
            {
                currencyValue = Mathf.RoundToInt(currencyValue * failedMod);
            }

            var amount = new CurrencyAmount(currencyKey, currencyValue);

            storeService.CurrenciesModule.AddCurrency(amount);

            return new CurrencyRewardResult
            {
                CurrencyKey = currencyKey,
                CurrencyValue = currencyValue
            };
        }
    }
}