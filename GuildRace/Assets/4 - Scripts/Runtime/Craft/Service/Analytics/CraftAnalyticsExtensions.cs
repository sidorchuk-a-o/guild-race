using AD.Services.Analytics;
using Game.Inventory;

namespace Game.Craft
{
    public static class CraftAnalyticsExtensions
    {
        public static void CreateItem(this IAnalyticsService analytics, ItemData item, int count)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddItem(item, count);

            analytics?.SendEvent("craft_item", parameters);
        }

        public static void RecycleItem(this IAnalyticsService analytics, ItemInfo item)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddItem(item);

            analytics?.SendEvent("recycle_item", parameters);
        }
    }
}