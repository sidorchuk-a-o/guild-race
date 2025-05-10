using System;

namespace Game.Inventory
{
    public class ItemsGridsBaseFactory : ItemsGridsFactory
    {
        public override Type DataType => typeof(ItemsGridBaseData);

        protected override ItemsGridInfo CreateInfo(string id, ItemsGridData data)
        {
            var baseGridData = data as ItemsGridBaseData;

            return new ItemsGridBaseInfo(id, baseGridData);
        }

        public override ItemsGridSM CreateGridSave(ItemsGridInfo info)
        {
            var baseGrid = info as ItemsGridBaseInfo;

            return new ItemsGridBaseSM(baseGrid, InventoryFactory);
        }

        protected override ItemsGridInfo ReadSave(ItemsGridData data, ItemsGridSM save)
        {
            var baseGridData = data as ItemsGridBaseData;
            var baseGridSave = save as ItemsGridBaseSM;

            return baseGridSave.GetValue(baseGridData, InventoryFactory);
        }
    }
}