using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstancesSM
    {
        [ES3Serializable] private List<InstanceSM> values;

        public InstancesSM(IEnumerable<InstanceInfo> values)
        {
            this.values = values
                .Select(x => new InstanceSM(x))
                .ToList();
        }

        public IEnumerable<InstanceInfo> GetValues(InstancesConfig config)
        {
            return values.Select(x => x.GetValue(config));
        }
    }
}