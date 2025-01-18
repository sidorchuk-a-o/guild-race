using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Craft
{
    [JsonObject(MemberSerialization.Fields)]
    public class CraftStateSM
    {
        public const string key = "craft";

        [ES3Serializable] private VendorsSM vendorsSM;

        public IVendorsCollection Vendors
        {
            set => vendorsSM = new(value);
        }

        public IEnumerable<VendorInfo> GetVendors(CraftConfig config)
        {
            return vendorsSM.GetValues(config);
        }
    }
}