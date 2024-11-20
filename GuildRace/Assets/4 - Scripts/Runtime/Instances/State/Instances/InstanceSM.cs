using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstanceSM
    {
        [ES3Serializable] private int id;

        public InstanceSM(InstanceInfo info)
        {
            id = info.Id;
        }

        public InstanceInfo GetValue(InstancesConfig config)
        {
            var data = config.GetInstance(id);

            return new(data);
        }
    }
}