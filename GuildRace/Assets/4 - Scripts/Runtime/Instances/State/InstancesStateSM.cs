using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstancesStateSM
    {
        public const string key = "instances";

        [ES3Serializable] private SeasonsSM seasonsSM;
        [ES3Serializable] private ActiveInstancesSM activeInstancesSM;
        [ES3Serializable] private int guaranteedCompletedCount;

        public int GuaranteedCompletedCount
        {
            get => guaranteedCompletedCount;
            set => guaranteedCompletedCount = value;
        }

        public void SetSeasons(IEnumerable<SeasonInfo> value)
        {
            seasonsSM = new(value);
        }

        public IEnumerable<SeasonInfo> GetSeasons(InstancesConfig config)
        {
            return seasonsSM.GetValues(config);
        }

        public void SetActiveInstances(IEnumerable<ActiveInstanceInfo> value, IInventoryService inventoryService)
        {
            activeInstancesSM = new(value, inventoryService);
        }

        public IEnumerable<ActiveInstanceInfo> GetActiveInstances(IInventoryService inventoryService, SeasonsCollection seasons)
        {
            return activeInstancesSM.GetValues(inventoryService, seasons);
        }
    }
}