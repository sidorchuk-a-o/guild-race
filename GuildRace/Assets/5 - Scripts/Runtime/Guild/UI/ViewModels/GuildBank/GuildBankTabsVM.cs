using AD.Game.Inventory;
using AD.Router;

namespace AD.Game.Guild
{
    public class GuildBankTabsVM : VMCollection<GuildBankTabInfo, GuildBankTabVM>
    {
        private readonly InventoryVMFactory inventoryVMF;

        public GuildBankTabVM this[ItemsGridCellType cellType] => GetOrCreate(x => x.Grid.CellType == cellType);

        public GuildBankTabsVM(IGuildBankTabsCollection values, InventoryVMFactory inventoryVMF) : base(values)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override GuildBankTabVM Create(GuildBankTabInfo value)
        {
            return new(value, inventoryVMF);
        }
    }
}