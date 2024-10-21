using AD.ToolsCollection;

namespace Game.Items
{
    public class ItemsFactory
    {
        private readonly ItemsConfig config;

        public ItemsFactory(ItemsConfig config)
        {
            this.config = config;
        }

        public ItemInfo CreateInfo(ItemData data)
        {
            var id = GuidUtils.Generate();

            return data switch
            {
                EquipItemData equip => new EquipItemInfo(id, equip),
                _ => null
            };
        }

        public ItemSM CreateSave(ItemInfo info)
        {
            return info switch
            {
                EquipItemInfo equip => new EquipItemSM(equip),
                _ => null
            };
        }

        public ItemInfo ReadSave(ItemSM save)
        {
            return save switch
            {
                EquipItemSM equip => CreateEquipInfo(equip),
                _ => null
            };
        }

        private EquipItemInfo CreateEquipInfo(EquipItemSM equip)
        {
            var equipData = config.EquipsParams.GetItem(equip.DataId);

            return equip.GetValue(equipData);
        }
    }
}