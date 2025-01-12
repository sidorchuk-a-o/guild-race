using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class SeasonsSM
    {
        [ES3Serializable] private List<SeasonSM> values;

        public SeasonsSM(IEnumerable<SeasonInfo> values)
        {
            this.values = values
                .Select(x => new SeasonSM(x))
                .ToList();
        }

        public IEnumerable<SeasonInfo> GetValues(InstancesConfig config)
        {
            return values.Select(x => x.GetValue(config));
        }
    }
}