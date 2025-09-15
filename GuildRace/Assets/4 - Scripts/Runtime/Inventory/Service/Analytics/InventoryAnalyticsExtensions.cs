using AD.Services.Analytics;

namespace Game.Inventory
{
    public static class InventoryAnalyticsExtensions
    {
        private static InventoryConfig InventoryConfig { get; set; }

        public static void Init(InventoryConfig inventoryConfig)
        {
            InventoryConfig = inventoryConfig;
        }

        public static void AddItems(this AnalyticsParams parameters, IItemsCollection items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var itemParams = AnalyticsParams.Empty;

                itemParams.AddItem(item);

                parameters[$"item_{i}"] = itemParams;
            }
        }

        public static void AddItem(this AnalyticsParams parameters, ItemInfo item)
        {
            var itemData = InventoryConfig.GetItem(item.DataId);

            parameters.AddItem(itemData);
        }

        public static void AddItem(this AnalyticsParams parameters, ItemData item)
        {
            parameters["item_id"] = item.Id;
            parameters["item_name"] = item.Title;
        }
    }
}