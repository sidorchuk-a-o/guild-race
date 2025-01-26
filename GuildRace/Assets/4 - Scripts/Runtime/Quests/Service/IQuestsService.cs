using System.Collections.Generic;

namespace Game.Quests
{
    public interface IQuestsService
    {
        IEnumerable<QuestInfo> Quests { get; }
        IEnumerable<QuestsGroupModule> Modules { get; }

        QuestsGroupModule GetModule(QuestsGroup id);
        QuestMechanicHandler GetMechanicHandler(int id);
    }
}