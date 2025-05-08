using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class UnitSM
    {
        [ES3Serializable] private int id;
        [ES3Serializable] private string instanceId;

        public int Id => id;

        public UnitSM(UnitInfo info)
        {
            id = info.Id;
            instanceId = info.InstanceId.Value;
        }

        public UnitInfo GetValue(UnitData data)
        {
            var info = new UnitInfo(data);

            info.SetInstanceId(instanceId);

            return info;
        }
    }
}