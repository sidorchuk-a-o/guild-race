using AD.ToolsCollection;
using AD.UI;
using Game.Craft;
using Game.Inventory;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class CreateConsumablesHandler : QuestMechanicHandler
    {
        private const int itemIdIndex = 0;

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
                var targetItemId = questParams[itemIdIndex].IntParse();

                if (targetItemId != craftingResult.ItemDataId)
                {
                    continue;
                }

                quest.AddProgress(craftingResult.Count);
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