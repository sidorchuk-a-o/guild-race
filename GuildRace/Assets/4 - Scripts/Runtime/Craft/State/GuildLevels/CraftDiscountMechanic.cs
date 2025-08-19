using AD.Services.Localization;
using AD.UI;
using Game.GuildLevels;
using UnityEngine;

namespace Game.Craft
{
    public class CraftDiscountMechanic : LevelMechanic
    {
        [SerializeField] private int discountValue;

        public override void Apply(LevelContext context)
        {
            if (context is CraftLevelContext craftContext)
            {
                var dicount = discountValue / 100f;

                craftContext.AddDiscount(dicount);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, discountValue);
        }
    }
}