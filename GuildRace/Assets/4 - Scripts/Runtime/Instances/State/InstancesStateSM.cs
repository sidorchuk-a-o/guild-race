using System;
using System.Collections.Generic;
using AD.ToolsCollection;
using Newtonsoft.Json;
using Game.Inventory;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstancesStateSM
    {
        public const string key = "instances";

        [ES3Serializable] private SeasonsSM seasonsSM;
        [ES3Serializable] private ActiveInstancesSM activeInstancesSM;
        [ES3Serializable] private int guaranteedCompletedCount;
        [ES3Serializable] private long lastResetDay;
        [ES3Serializable] private int lastResetWeek;

        public int GuaranteedCompletedCount
        {
            get => guaranteedCompletedCount;
            set => guaranteedCompletedCount = value;
        }

        public DateTime LastResetDay
        {
            get => lastResetDay.FromUnixTimestamp();
            set => lastResetDay = value.ToUnixTimestamp();
        }

        public int LastResetWeek
        {
            get => lastResetWeek;
            set => lastResetWeek = value;
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