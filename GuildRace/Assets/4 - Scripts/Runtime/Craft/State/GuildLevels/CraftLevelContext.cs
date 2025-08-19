using Game.GuildLevels;
using UniRx;

namespace Game.Craft
{
    public class CraftLevelContext : LevelContext
    {
        private readonly ReactiveProperty<float> discount = new();

        public IReadOnlyReactiveProperty<float> Discount => discount;

        public void AddDiscount(float percent)
        {
            discount.Value += percent;
        }
    }
}