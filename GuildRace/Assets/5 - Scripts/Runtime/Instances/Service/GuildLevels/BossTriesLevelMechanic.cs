using AD.Game.GuildLevels;
using AD.Localization;
using AD.UI;
using UnityEngine;

namespace AD.Game.Instances
{
    public class BossTriesLevelMechanic : LevelMechanic
    {
        [SerializeField] private InstanceType instanceType;
        [SerializeField] private int increaseValue;

        public override void Apply(LevelContext context)
        {
            if (context is InstancesLevelContext instancesContext)
            {
                instancesContext.AddBossTries(instanceType, increaseValue);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, increaseValue);
        }
    }
}