using AD.Game.GuildLevels;
using AD.Localization;
using AD.UI;
using UnityEngine;

namespace AD.Game.Guild
{
    public class CharactersCountLevelMechanic : LevelMechanic
    {
        [SerializeField] private int increaseValue;

        public override void Apply(LevelContext context)
        {
            if (context is GuildLevelContext guildContext)
            {
                guildContext.AddCharactersCount(increaseValue);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, increaseValue);
        }
    }
}