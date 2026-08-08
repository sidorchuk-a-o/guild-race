using AD.Game.GuildLevels;
using R3;

namespace AD.Game.Craft
{
    public class CraftLevelContext : LevelContext
    {
        private readonly ReactiveProperty<float> discount = new();

        public ReadOnlyReactiveProperty<float> Discount => discount;

        public void AddDiscount(float percent)
        {
            discount.Value += percent;
        }
    }
}