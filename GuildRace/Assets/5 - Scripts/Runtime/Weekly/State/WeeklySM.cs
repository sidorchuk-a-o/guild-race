using Newtonsoft.Json;

namespace AD.Game.Weekly
{
    [JsonObject(MemberSerialization.Fields)]
    public class WeeklySM
    {
        [ES3Serializable] private int currentWeek;

        public int CurrentWeek
        {
            get => currentWeek;
            set => currentWeek = value;
        }
    }
}