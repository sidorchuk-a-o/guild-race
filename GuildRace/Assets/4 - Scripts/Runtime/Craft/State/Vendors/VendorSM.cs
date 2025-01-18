using Newtonsoft.Json;

namespace Game.Craft
{
    [JsonObject(MemberSerialization.Fields)]
    public class VendorSM
    {
        [ES3Serializable] private int id;

        public VendorSM(VendorInfo info)
        {
            id = info.Id;
        }

        public VendorInfo GetValue(CraftConfig config)
        {
            var data = config.GetVendor(id);

            return new VendorInfo(data);
        }
    }
}