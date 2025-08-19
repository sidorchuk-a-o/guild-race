using AD.Services.Localization;
using AD.UI;
using Game.GuildLevels;
using UnityEngine;

namespace Game.Quests
{
    public class QuestCountLevelMechanic : LevelMechanic
    {
        [SerializeField] private QuestsGroup questsGroup;
        [SerializeField] private int increaseValue;

        public override void Apply(LevelContext context)
        {
            if (context is QuestLevelContext questContext)
            {
                questContext.AddQuestCount(questsGroup, increaseValue);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, increaseValue);
        }
    }
}