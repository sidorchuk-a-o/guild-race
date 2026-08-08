using AD.Game.GuildLevels;
using ObservableCollections;
using R3;

namespace AD.Game.Quests
{
    public class QuestLevelContext : LevelContext
    {
        private readonly ObservableDictionary<QuestsGroup, int> questCounts = new();
        private readonly ReactiveProperty<float> rewardBonus = new();

        public IReadOnlyObservableDictionary<QuestsGroup, int> QuestCounts => questCounts;
        public ReadOnlyReactiveProperty<float> RewardBonus => rewardBonus;

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