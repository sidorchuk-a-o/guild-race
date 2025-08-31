using AD.Services.Localization;
using AD.UI;
using Game.GuildLevels;
using UnityEngine;

namespace Game.Guild
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