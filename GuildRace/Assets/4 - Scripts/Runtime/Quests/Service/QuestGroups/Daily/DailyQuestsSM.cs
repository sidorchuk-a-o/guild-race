using AD.ToolsCollection;
using Newtonsoft.Json;
using System;

namespace Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class DailyQuestsSM : QuestsGroupSM
    {
        public const string key = "daily-quests";

        [ES3Serializable] private long lastResetDate;

        public DateTime LastResetDate
        {
            get => lastResetDate.FromUnixTimestamp();
            set => lastResetDate = value.ToUnixTimestamp();
        }
    }
}