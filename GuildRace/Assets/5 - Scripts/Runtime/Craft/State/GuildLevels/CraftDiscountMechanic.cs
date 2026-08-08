using AD.Game.GuildLevels;
using AD.Localization;
using AD.UI;
using UnityEngine;

namespace AD.Game.Craft
{
    public class CraftDiscountMechanic : LevelMechanic
    {
        [SerializeField] private int discountValue;

        public override void Apply(LevelContext context)
        {
            if (context is CraftLevelContext craftContext)
            {
                var discount = discountValue / 100f;

                craftContext.AddDiscount(discount);
            }
        }

        public override UITextData GetDesc(LocalizeKey descKey)
        {
            return new(descKey, discountValue);
        }
    }
}