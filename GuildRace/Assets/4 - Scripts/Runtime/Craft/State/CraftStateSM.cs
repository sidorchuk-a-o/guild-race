using Newtonsoft.Json;

namespace Game.Craft
{
    [JsonObject(MemberSerialization.Fields)]
    public class CraftStateSM
    {
        public const string key = "craft";
    }
}