using Newtonsoft.Json;

namespace AD.Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class WeeklyQuestsSM : QuestsGroupSM
    {
        [ES3Serializable] private int lastResetWeek;

        public int LastResetWeek
        {
            get => lastResetWeek;
            set => lastResetWeek = value;
        }
    }
}