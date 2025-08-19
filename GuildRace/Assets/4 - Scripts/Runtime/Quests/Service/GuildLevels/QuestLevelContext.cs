using Game.GuildLevels;
using UniRx;

namespace Game.Quests
{
    public class QuestLevelContext : LevelContext
    {
        private readonly ReactiveDictionary<QuestsGroup, int> questCounts = new();
        private readonly ReactiveProperty<float> rewardBonus = new();

        public IReadOnlyReactiveDictionary<QuestsGroup, int> QuestCounts => questCounts;
        public IReadOnlyReactiveProperty<float> RewardBonus => rewardBonus;

        public QuestLevelContext(QuestsConfig config)
        {
            foreach (var questGroup in config.GroupModules)
            {
                questCounts[questGroup.Group] = 0;
            }
        }

        public void AddQuestCount(QuestsGroup group, int count)
        {
            questCounts[group] += count;
        }

        public void AddRewardBonus(float value)
        {
            rewardBonus.Value += value;
        }
    }
}