using System.Collections.Generic;
using R3;

namespace AD.Game.Quests
{
    public interface IQuestsService
    {
        IEnumerable<QuestInfo> Quests { get; }
        IEnumerable<QuestsGroupModule> Modules { get; }
        ReadOnlyReactiveProperty<float> RewardBonus { get; }

        QuestsGroupModule GetGroupModule(QuestsGroup group);
        QuestMechanicHandler GetMechanicHandler(int id);

        void TakeQuestReward(TakeRewardArgs args);
    }
}