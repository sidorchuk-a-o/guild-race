using System;

namespace Game.Inventory
{
    public class EquipItemsFactory : ItemsFactory
    {
        public override Type DataType { get; } = typeof(EquipItemData);

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var equipData = data as EquipItemData;
            var equip = new EquipItemInfo(id, equipData, ItemType);

            equip.SetGridParams(InventoryConfig.EquipsParams.GridParams);

            return equip;
        }

        public override ItemSM CreateSave(ItemInfo info)
        {
            return new EquipItemSM(info as EquipItemInfo);
        }

        protected override ItemInfo ReadSave(ItemData data, ItemSM save)
        {
            var equipData = data as EquipItemData;
            var equipSave = save as EquipItemSM;

            var equip = equipSave.GetValue(equipData, this);

            equip.SetGridParams(InventoryConfig.EquipsParams.GridParams);

            return equip;
        }
    }
}