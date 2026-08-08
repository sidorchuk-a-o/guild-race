using AD.Game.GuildLevels;
using ObservableCollections;

namespace AD.Game.Instances
{
    public class InstancesLevelContext : LevelContext
    {
        private readonly ObservableDictionary<InstanceType, int> bossTriesMods = new();

        public IReadOnlyObservableDictionary<InstanceType, int> BossTriesMods => bossTriesMods;

        public InstancesLevelContext(InstancesConfig config)
        {
            foreach (var type in config.InstanceTypes)
            {
                bossTriesMods[type.Type] = 0;
            }
        }

        public void AddBossTries(InstanceType instanceType, int increaseValue)
        {
            bossTriesMods[instanceType] += increaseValue;
        }
    }
}