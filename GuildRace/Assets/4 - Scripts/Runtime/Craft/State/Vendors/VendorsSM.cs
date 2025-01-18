using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Craft
{
    [JsonObject(MemberSerialization.Fields)]
    public class VendorsSM
    {
        [ES3Serializable] private List<VendorSM> values;

        public VendorsSM(IEnumerable<VendorInfo> values)
        {
            this.values = values
                .Select(x => new VendorSM(x))
                .ToList();
        }

        public IEnumerable<VendorInfo> GetValues(CraftConfig config)
        {
            return values.Select(x => x.GetValue(config));
        }
    }
}