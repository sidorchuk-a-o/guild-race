using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class QuestSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private int dataId;
        [ES3Serializable] private int progress;
        [ES3Serializable] private bool isRewarded;
        [ES3Serializable] private Dictionary<string, string> data;

        public QuestSM(QuestInfo info)
        {
            id = info.Id;
            dataId = info.DataId;
            progress = info.ProgressCounter.Value;
            isRewarded = info.IsRewarded.Value;
            data = new(info.Data);
        }

        public QuestInfo GetValue(QuestsConfig config)
        {
            var questData = config.GetQuest(dataId);
            var quest = new QuestInfo(id, questData);

            quest.SetProgress(progress);

            if (isRewarded)
            {
                quest.MarkAsRewarded();
            }

            foreach (var value in data)
            {
                quest.AddData(value.Key, value.Value);
            }

            return quest;
        }
    }
}