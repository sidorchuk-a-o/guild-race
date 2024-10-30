using AD.Services.Router;
using Game.Inventory;

namespace Game.Guild
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
            return new GuildBankTabVM(value, inventoryVMF);
        }
    }
}