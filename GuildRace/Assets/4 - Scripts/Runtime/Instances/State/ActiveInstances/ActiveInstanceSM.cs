using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class ActiveInstanceSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private int instanceId;
        [ES3Serializable] private int bossUnitId;
        [ES3Serializable] private List<SquadUnitSM> squadSM;
        [ES3Serializable] private long startTime;
        [ES3Serializable] private float completeChance;
        [ES3Serializable] private bool isReadyToComplete;

        public ActiveInstanceSM(ActiveInstanceInfo info, IInventoryService inventoryService)
        {
            id = info.Id;
            instanceId = info.Instance.Id;
            bossUnitId = info.BossUnit.Id;
            startTime = info.StartTime;
            completeChance = info.CompleteChance.Value;
            isReadyToComplete = info.IsReadyToComplete.Value;

            squadSM = info.Squad
                .Select(x => new SquadUnitSM(x, inventoryService))
                .ToList();
        }

        public ActiveInstanceInfo GetValue(IInventoryService inventoryService, SeasonsCollection seasons)
        {
            var instance = seasons.GetInstance(instanceId);
            var bossUnit = instance.GetBossUnit(bossUnitId);
            var squad = squadSM.Select(x => x.GetValue(inventoryService));
            var activeInstance = new ActiveInstanceInfo(id, instance, bossUnit, squad);

            activeInstance.SetStartTime(startTime);
            activeInstance.SetCompleteChance(completeChance);

            if (isReadyToComplete)
            {
                activeInstance.MarAsReadyToComplete();
            }

            return activeInstance;
        }
    }
}