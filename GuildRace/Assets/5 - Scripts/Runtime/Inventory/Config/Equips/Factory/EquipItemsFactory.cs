using System;

namespace AD.Game.Inventory
{
    public class EquipItemsFactory : ItemsFactory
    {
        public override Type DataType { get; } = typeof(EquipItemData);

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var equipData = data as EquipItemData;
            var equip = new EquipItemInfo(id, equipData);

            equip.SetGridParams(InventoryConfig.EquipsParams.GridParams);

            return equip;
        }

        public override ItemSM CreateSave(ItemInfo info)
        {
            return new EquipItemSM(info as EquipItemInfo);
        }

        protected override ItemInfo ReadSave(ItemData data, ItemSM save)
        {
            var equipData = (EquipItemData)data;
            var equipSave = (EquipItemSM)save;

            var equip = equipSave.GetValue(equipData, this);

            equip.SetGridParams(InventoryConfig.EquipsParams.GridParams);

            return equip;
        }
    }
}