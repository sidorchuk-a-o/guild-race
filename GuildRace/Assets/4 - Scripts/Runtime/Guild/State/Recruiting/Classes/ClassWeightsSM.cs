using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class ClassWeightsSM
    {
        [ES3Serializable] private List<ClassWeightSM> values;

        public ClassWeightsSM(IEnumerable<ClassWeightInfo> values)
        {
            this.values = values
                .Select(x => new ClassWeightSM(x))
                .ToList();
        }

        public IEnumerable<ClassWeightInfo> GetValues()
        {
            return values.Select(x => x.GetValue());
        }
    }
}