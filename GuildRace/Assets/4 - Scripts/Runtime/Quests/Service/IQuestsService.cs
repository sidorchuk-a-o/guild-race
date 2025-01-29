using System.Collections.Generic;

namespace Game.Quests
{
    public interface IQuestsService
    {
        IEnumerable<QuestInfo> Quests { get; }
        IEnumerable<QuestsGroupModule> Modules { get; }

        QuestsGroupModule GetGroupModule(QuestsGroup group);
        QuestMechanicHandler GetMechanicHandler(int id);

        void TakeQuestReward(TakeRewardArgs args);
    }
}