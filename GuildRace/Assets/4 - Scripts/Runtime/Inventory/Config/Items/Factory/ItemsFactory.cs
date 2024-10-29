using AD.ToolsCollection;
using System;
using VContainer;

namespace Game.Inventory
{
    public abstract class ItemsFactory : ScriptableData
    {
        public abstract Type DataType { get; }
        public InventoryConfig Config { get; private set; }

        [Inject]
        public void Inject(InventoryConfig config)
        {
            Config = config;
        }

        // == Info ==

        public ItemInfo CreateInfo(ItemData data)
        {
            var id = GuidUtils.Generate();

            return CreateInfo(id, data);
        }

        protected abstract ItemInfo CreateInfo(string id, ItemData data);

        // == Save ==

        public abstract ItemSM CreateSave(ItemInfo info);

        public ItemInfo ReadSave(ItemSM save)
        {
            var data = Config.GetItem(save.DataId);

            return ReadSave(data, save);
        }

        protected abstract ItemInfo ReadSave(ItemData data, ItemSM save);
    }
}