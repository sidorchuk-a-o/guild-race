using AD.Game.Craft;
using AD.Game.Inventory;
using AD.ToolsCollection;
using AD.UI;
using R3;
using VContainer;

namespace AD.Game.Quests
{
    public class CreateConsumablesHandler : QuestMechanicHandler
    {
        private const int ItemIdIndex = 0;

        private InventoryConfig inventoryConfig;
        private ICraftService craftService;

        [Inject]
        public void Inject(InventoryConfig inventoryConfig, ICraftService craftService)
        {
            this.inventoryConfig = inventoryConfig;
            this.craftService = craftService;
        }

        public override void Init()
        {
            base.Init();

            craftService.OnCraftingComplete.Subscribe(CraftingCompleteCallback);
        }

        private void CraftingCompleteCallback(CraftingResult craftingResult)
        {
            foreach (var quest in Quests)
            {
                var questParams = quest.MechanicParams;
                var targetItemId = questParams[ItemIdIndex].IntParse();

                if (targetItemId != craftingResult.ItemDataId)
                {
                    continue;
                }

                quest.AddProgress(craftingResult.Count);
            }
        }

        public override UITextData GetDescKey(QuestInfo quest)
        {
            var itemId = quest.MechanicParams[ItemIdIndex].IntParse();
            var itemData = inventoryConfig.GetItem(itemId);

            var data = new object[]
            {
                quest.RequiredProgress,
                itemData.NameKey
            };

            return new(descKey, data);
        }
    }
}