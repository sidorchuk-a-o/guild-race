using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using AD.UI;
using Game.Instances;
using Game.Inventory;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class RewardedReagentsHandler : QuestMechanicHandler
    {
        private const int itemIdIndex = 0;

        private InventoryConfig inventoryConfig;
        private IInstancesService instancesService;

        [Inject]
        public void Inject(InventoryConfig inventoryConfig, IInstancesService instancesService)
        {
            this.inventoryConfig = inventoryConfig;
            this.instancesService = instancesService;
        }

        public override void Init()
        {
            base.Init();

            instancesService.OnRewardsReceived.Subscribe(RewardsReceivedCallback);
        }

        private void RewardsReceivedCallback(IEnumerable<RewardResult> rewards)
        {
            var reagents = rewards.OfType<ReagentRewardResult>();

            foreach (var quest in Quests)
            {
                var itemId = quest.MechanicParams[itemIdIndex].IntParse();
                var reagentCount = reagents.Where(x => x.ItemDataId == itemId).Sum(x => x.Count);

                quest.AddProgress(reagentCount);
            }
        }

        public override UITextData GetDescKey(QuestInfo quest)
        {
            var itemId = quest.MechanicParams[itemIdIndex].IntParse();
            var itemData = inventoryConfig.GetItem(itemId);

            var data = new object[]
            {
                quest.RequiredProgress,
                itemData.NameKey
            };

            return new UITextData(descKey, data);
        }
    }
}