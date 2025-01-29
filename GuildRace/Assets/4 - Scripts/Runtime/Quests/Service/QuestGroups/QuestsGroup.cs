using AD.ToolsCollection;
using System;

namespace Game.Quests
{
    [Serializable]
    public class QuestsGroup : Key<int>
    {
        public QuestsGroup()
        {
        }

        public QuestsGroup(int key) : base(key)
        {
        }

        public static implicit operator int(QuestsGroup key) => key?.value ?? -1;
        public static implicit operator QuestsGroup(int key) => new(key);
    }
}