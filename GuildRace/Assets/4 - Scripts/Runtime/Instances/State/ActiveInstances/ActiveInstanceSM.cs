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
        [ES3Serializable] private long startTime;
        [ES3Serializable] private bool isReadyToComplete;

        public ActiveInstanceSM(ActiveInstanceInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            instanceId = info.Instance.Id;
            squad = new(info.Squad);
            startTime = info.StartTime;
            isReadyToComplete = info.IsReadyToComplete.Value;
        }

        public ActiveInstanceInfo GetValue(IInventoryService inventoryService, SeasonsCollection seasons)
        {
            var instance = seasons.GetInstance(instanceId);
            var activeInstance = new ActiveInstanceInfo(id, instance, squad);

            activeInstance.SetStartTime(startTime);

            if (isReadyToComplete)
            {
                activeInstance.MarAsReadyToComplete();
            }

            return activeInstance;
        }
    }
}