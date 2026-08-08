using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Quests
{
    [JsonObject(MemberSerialization.Fields)]
    public class QuestsSM
    {
        [ES3Serializable] private List<QuestSM> values;

        public QuestsSM(IEnumerable<QuestInfo> values)
        {
            this.values = values
                .Select(x => new QuestSM(x))
                .ToList();
        }

        public IEnumerable<QuestInfo> GetValues(QuestsConfig config)
        {
            return values.Select(x => x.GetValue(config));
        }
    }
}