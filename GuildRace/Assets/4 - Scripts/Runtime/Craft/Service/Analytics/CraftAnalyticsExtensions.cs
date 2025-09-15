using AD.Services.Analytics;
using AD.Services.Store;
using Game.Inventory;

namespace Game.Craft
{
    public static class CraftAnalyticsExtensions
    {
        public static void CreateItem(this IAnalyticsService analytics, ItemData item, int count, CurrencyAmount price)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddItem(item);
            parameters.AddCurrencyAmount(price.Key);
            parameters["price"] = price.Value.ToString();
            parameters["count"] = count;

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