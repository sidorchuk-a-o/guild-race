using Game.GuildLevels;
using UniRx;

namespace Game.Instances
{
    public class InstancesLevelContext : LevelContext
    {
        private readonly ReactiveDictionary<InstanceType, int> bossTriesMods = new();

        public IReadOnlyReactiveDictionary<InstanceType, int> BossTriesMods => bossTriesMods;

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