using AD.Game.GuildLevels;
using AD.Localization;
using AD.UI;
using UnityEngine;

namespace AD.Game.Quests
{
    public class QuestBonusLevelMechanic : LevelMechanic
    {
        [SerializeField] private int bonusPercent;

        public override void Apply(LevelContext context)
        {
            if (context is QuestLevelContext questContext)
            {
                var bonus = bonusPercent / 100f;

                questContext.AddRewardBonus(bonus);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, bonusPercent);
        }
    }
}