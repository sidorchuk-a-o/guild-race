using System.Collections.Generic;
using UniRx;

namespace Game.Quests
{
    public interface IQuestsService
    {
        IEnumerable<QuestInfo> Quests { get; }
        IEnumerable<QuestsGroupModule> Modules { get; }
        IReadOnlyReactiveProperty<float> RewardBonus { get; }

        QuestsGroupModule GetGroupModule(QuestsGroup group);
        QuestMechanicHandler GetMechanicHandler(int id);

        void TakeQuestReward(TakeRewardArgs args);
    }
}