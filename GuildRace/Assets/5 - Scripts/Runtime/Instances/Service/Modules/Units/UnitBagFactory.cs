using System;
using AD.Game.Inventory;

namespace AD.Game.Instances
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
            var baseGridData = (UnitBagData)data;
            var baseGridSave = (UnitBagSM)save;

            return baseGridSave.GetValue(baseGridData, InventoryFactory);
        }
    }
}