using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class ActiveInstanceSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private int instanceId;
        [ES3Serializable] private List<string> squad;
        [ES3Serializable] private ItemsGridSM bagSM;

        public ActiveInstanceSM(ActiveInstanceInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            instanceId = info.InstanceId;
            squad = new(info.Squad);
            bagSM = inventoryService.Factory.CreateGridSave(info.Bag);
        }

        public ActiveInstanceInfo GetValue(IInventoryService inventoryService)
        {
            var bag = inventoryService.Factory.ReadGridSave(bagSM);

            return new ActiveInstanceInfo(id, instanceId, bag, squad);
        }
    }
}