using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class SquadUnitSM
    {
        [ES3Serializable] private string charactedId;
        [ES3Serializable] private ItemsGridSM bagSM;

        public SquadUnitSM(SquadUnitInfo info, IInventoryService inventoryService)
        {
            charactedId = info.CharactedId;
            bagSM = inventoryService.Factory.CreateGridSave(info.Bag);
        }

        public SquadUnitInfo GetValue(IInventoryService inventoryService)
        {
            var bag = inventoryService.Factory.ReadGridSave(bagSM);

            return new SquadUnitInfo(charactedId, bag, resolvedThreats: null);
        }
    }
}