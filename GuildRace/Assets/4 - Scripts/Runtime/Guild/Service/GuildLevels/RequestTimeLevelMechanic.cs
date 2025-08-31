using AD.Services.Localization;
using AD.UI;
using Game.GuildLevels;
using UnityEngine;

namespace Game.Guild
{
    public class RequestTimeLevelMechanic : LevelMechanic
    {
        [SerializeField] private int decreasePercent;

        public override void Apply(LevelContext context)
        {
            if (context is GuildLevelContext guildContext)
            {
                var percent = decreasePercent / 100f;

                guildContext.AddRequestTimePercent(percent);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, decreasePercent);
        }
    }
}