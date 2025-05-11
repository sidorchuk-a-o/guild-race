using System.Collections.Generic;
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

        public override void ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result)
        {
            rewards.ForEach(x => ApplyReward(x, result));
        }

        public override void ApplyReward(InstanceRewardData reward, CompleteResult result)
        {
            var currencyKey = new CurrencyKey(reward.MechanicParams[0]);
            var currencyValue = reward.MechanicParams[1].IntParse();

            if (result != CompleteResult.Completed)
            {
                currencyValue = Mathf.RoundToInt(currencyValue * failedMod);
            }

            var amount = new CurrencyAmount(currencyKey, currencyValue);

            storeService.CurrenciesModule.AddCurrency(amount);
        }
    }
}