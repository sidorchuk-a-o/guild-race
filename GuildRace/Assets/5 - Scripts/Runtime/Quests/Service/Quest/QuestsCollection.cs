using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Quests
{
    public class QuestsCollection : ReactiveCollection<QuestInfo>, IQuestsCollection
    {
        public QuestsCollection(IEnumerable<QuestInfo> values = null) : base(values)
        {
        }
    }
}