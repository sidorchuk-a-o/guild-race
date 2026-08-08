using System;
using AD.ToolsCollection;
using Newtonsoft.Json;

namespace AD.Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class DailyQuestsSM : QuestsGroupSM
    {
        [ES3Serializable] private long lastResetDate;

        public DateTime LastResetDate
        {
            get => lastResetDate.FromUnixTimestamp();
            set => lastResetDate = value.ToUnixTimestamp();
        }
    }
}