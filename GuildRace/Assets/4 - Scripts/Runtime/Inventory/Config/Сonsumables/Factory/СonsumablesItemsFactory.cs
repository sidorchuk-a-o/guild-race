using System;

namespace Game.Inventory
{
    public class СonsumablesItemsFactory : ItemsFactory
    {
        public override Type DataType { get; } = typeof(СonsumablesItemData);

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var consumablesData = data as СonsumablesItemData;
            var consumables = new СonsumablesItemInfo(id, consumablesData);

            consumables.SetGridParams(InventoryConfig.ConsumablesParams.GridParams);

            return consumables;
        }

        public override ItemSM CreateSave(ItemInfo info)
        {
            return new СonsumablesItemSM(info as СonsumablesItemInfo);
        }

        protected override ItemInfo ReadSave(ItemData data, ItemSM save)
        {
            var consumablesData = data as СonsumablesItemData;
            var consumablesSave = save as СonsumablesItemSM;

            var consumables = consumablesSave.GetValue(consumablesData);

            consumables.SetGridParams(InventoryConfig.ConsumablesParams.GridParams);

            return consumables;
        }
    }
}