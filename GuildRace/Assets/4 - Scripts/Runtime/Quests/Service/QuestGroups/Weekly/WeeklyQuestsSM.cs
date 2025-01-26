using Newtonsoft.Json;

namespace Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class WeeklyQuestsSM : QuestsGroupSM
    {
        public const string key = "weekly-quests";

        [ES3Serializable] private int lastResetWeek;

        public int LastResetWeek
        {
            get => lastResetWeek;
            set => lastResetWeek = value;
        }
    }
}