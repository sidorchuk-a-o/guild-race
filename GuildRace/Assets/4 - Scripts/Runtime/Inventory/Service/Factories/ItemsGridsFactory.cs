using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public abstract class ItemsGridsFactory : ScriptableData
    {
        public abstract Type DataType { get; }

        protected InventoryState State { get; private set; }
        protected InventoryConfig Config { get; private set; }
        protected IInventoryFactory InventoryFactory { get; private set; }

        public void Init(InventoryState state, InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            State = state;
            Config = config;
            InventoryFactory = inventoryFactory;
        }

        // == Info ==

        public ItemsGridInfo CreateGrid(ItemsGridData data)
        {
            var id = GuidUtils.Generate();
            var info = CreateInfo(id, data);

            State.AddGrid(info);

            return info;
        }

        protected abstract ItemsGridInfo CreateInfo(string id, ItemsGridData data);

        // == Save ==

        public abstract ItemsGridSM CreateGridSave(ItemsGridInfo info);

        public ItemsGridInfo ReadGridSave(ItemsGridSM save)
        {
            if (save == null)
            {
                return null;
            }

            var data = Config.GetGrid(save.DataId);
            var info = ReadSave(data, save);

            State.AddGrid(info);

            return info;
        }

        protected abstract ItemsGridInfo ReadSave(ItemsGridData data, ItemsGridSM save);
    }
}