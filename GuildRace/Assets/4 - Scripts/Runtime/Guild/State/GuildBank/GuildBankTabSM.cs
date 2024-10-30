using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildBankTabSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private ItemsGridSM gridSM;

        public GuildBankTabSM(GuildBankTabInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            gridSM = inventoryService.Factory.CreateGridSave(info.Grid);
        }

        public GuildBankTabInfo GetValue(GuildConfig config, IInventoryService inventoryService)
        {
            var data = config.GuildBankParams.GetTab(id);
            var grid = inventoryService.Factory.ReadGridSave(gridSM);

            return new GuildBankTabInfo(data, grid);
        }
    }
}