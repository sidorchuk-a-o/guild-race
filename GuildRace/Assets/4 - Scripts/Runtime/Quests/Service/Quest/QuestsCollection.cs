using AD.States;
using System.Collections.Generic;

namespace Game.Quests
{
    public class QuestsCollection : ReactiveCollectionInfo<QuestInfo>, IQuestsCollection
    {
        public QuestsCollection(IEnumerable<QuestInfo> values = null) : base(values)
        {
        }
    }
}