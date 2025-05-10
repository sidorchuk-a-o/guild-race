using System.Linq;
using AD.ToolsCollection;
using Game.Guild;
using Game.Inventory;
using VContainer;

namespace Game.Instances
{
    public class EquipRewardHandler : RewardHandler
    {
        private InventoryConfig inventoryConfig;

        private IGuildService guildService;
        private IInventoryService inventoryService;

        [Inject]
        public void Inject(InventoryConfig inventoryConfig, IGuildService guildService, IInventoryService inventoryService)
        {
            this.inventoryConfig = inventoryConfig;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        public override void ApplyReward(InstanceRewardData reward)
        {
            var equipCellTypes = inventoryConfig.EquipsParams.GridParams.CellTypes;
            var equipBank = guildService.BankTabs.FirstOrDefault(x =>
            {
                return equipCellTypes.Contains(x.Grid.CellType);
            });

            if (equipBank == null)
            {
                return;
            }

            var equipId = reward.MechanicParams[0].IntParse();
            var equipItem = inventoryService.Factory.CreateItem(equipId);

            inventoryService.TryPlaceItem(new PlaceInPlacementArgs
            {
                PlacementId = equipBank.Grid.Id,
                ItemId = equipItem.Id
            });
        }
    }
}