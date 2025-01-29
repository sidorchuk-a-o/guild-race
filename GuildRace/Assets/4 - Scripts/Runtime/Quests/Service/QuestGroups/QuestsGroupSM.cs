using System.Collections.Generic;

namespace Game.Quests
{
    public abstract class QuestsGroupSM
    {
        [ES3Serializable] private QuestsSM questsSM;

        public IEnumerable<QuestInfo> Quests
        {
            set => questsSM = new(value);
        }

        public IEnumerable<QuestInfo> GetQuests(QuestsConfig config)
        {
            return questsSM.GetValues(config);
        }
    }
}