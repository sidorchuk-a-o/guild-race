using System;
using AD.Game.Inventory;
using VContainer;

namespace AD.Game.Instances
{
    public class ConsumablesItemsFactory : ItemsFactory
    {
        private InstancesConfig config;

        public override Type DataType { get; } = typeof(ConsumablesItemData);

        [Inject]
        public void Inject(InstancesConfig config)
        {
            this.config = config;
        }

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var consumablesData = data as ConsumablesItemData;
            var consumables = new ConsumablesItemInfo(id, consumablesData);

            consumables.SetGridParams(config.ConsumablesParams.GridParams);

            return consumables;
        }

        public override ItemSM CreateSave(ItemInfo info)
        {
            return new ConsumablesItemSM(info as ConsumablesItemInfo);
        }

        protected override ItemInfo ReadSave(ItemData data, ItemSM save)
        {
            var consumablesData = (ConsumablesItemData)data;
            var consumablesSave = (ConsumablesItemSM)save;

            var consumables = consumablesSave.GetValue(consumablesData, this);

            consumables.SetGridParams(config.ConsumablesParams.GridParams);

            return consumables;
        }
    }
}