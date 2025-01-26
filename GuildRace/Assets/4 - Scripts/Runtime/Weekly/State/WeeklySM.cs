using Newtonsoft.Json;

namespace Game.Weekly
{
    [JsonObject(MemberSerialization.Fields)]
    public class WeeklySM
    {
        public const string key = "weekly";

        [ES3Serializable] private int currentWeek;

        public int CurrentWeek
        {
            get => currentWeek;
            set => currentWeek = value;
        }
    }
}