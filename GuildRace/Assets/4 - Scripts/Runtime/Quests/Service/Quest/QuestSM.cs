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

        public QuestSM(QuestInfo info)
        {
            id = info.Id;
            dataId = info.DataId;
            progress = info.ProgressCounter.Value;
            isRewarded = info.IsRewarded.Value;
        }

        public QuestInfo GetValue(QuestsConfig config)
        {
            var data = config.GetQuest(dataId);
            var info = new QuestInfo(id, data);

            info.SetProgress(progress);

            if (isRewarded)
            {
                info.MarkAsRewarded();
            }

            return info;
        }
    }
}