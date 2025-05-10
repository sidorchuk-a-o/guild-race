using System;
using Game.Inventory;

namespace Game.Instances
{
    public class UnitBagFactory : ItemsGridsFactory
    {
        public override Type DataType => typeof(UnitBagData);

        protected override ItemsGridInfo CreateInfo(string id, ItemsGridData data)
        {
            var baseGridData = data as UnitBagData;

            return new UnitBagInfo(id, baseGridData);
        }

        public override ItemsGridSM CreateGridSave(ItemsGridInfo info)
        {
            var baseGrid = info as UnitBagInfo;

            return new UnitBagSM(baseGrid, InventoryFactory);
        }

        protected override ItemsGridInfo ReadSave(ItemsGridData data, ItemsGridSM save)
        {
            var baseGridData = data as UnitBagData;
            var baseGridSave = save as UnitBagSM;

            return baseGridSave.GetValue(baseGridData, InventoryFactory);
        }
    }
}