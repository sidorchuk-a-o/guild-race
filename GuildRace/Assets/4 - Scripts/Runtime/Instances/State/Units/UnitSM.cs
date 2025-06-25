using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class UnitSM
    {
        [ES3Serializable] private int id;
        [ES3Serializable] private string instanceId;
        [ES3Serializable] private int totalCompletedCount;
        [ES3Serializable] private int completedCount;
        [ES3Serializable] private int triesCount;

        public int Id => id;

        public UnitSM(UnitInfo info)
        {
            id = info.Id;
            instanceId = info.InstanceId.Value;
            totalCompletedCount = info.TotalCompletedCount;
            completedCount = info.CompletedCount.Value;
            triesCount = info.TriesCount.Value;
        }

        public UnitInfo GetValue(UnitData data)
        {
            var info = new UnitInfo(data);

            info.SetInstanceId(instanceId);
            info.SetTriesCount(triesCount);
            info.SetCompletedCount(totalCompletedCount, completedCount);

            return info;
        }
    }
}