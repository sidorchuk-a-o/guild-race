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
                parameters.AddItem(items[i]);
            }
        }

        public static void AddItem(this AnalyticsParams parameters, ItemInfo item, object value = null)
        {
            var itemData = InventoryConfig.GetItem(item.DataId);

            parameters.AddItem(itemData, value);
        }

        public static void AddItem(this AnalyticsParams parameters, ItemData item, object value = null)
        {
            parameters[item.Title] = value ?? string.Empty;
        }
    }
}