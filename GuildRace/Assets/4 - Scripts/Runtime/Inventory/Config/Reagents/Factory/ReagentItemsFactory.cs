using System;

namespace Game.Inventory
{
    public class ReagentItemsFactory : ItemsFactory
    {
        public override Type DataType { get; } = typeof(ReagentItemData);

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var reagentData = data as ReagentItemData;
            var reagent = new ReagentItemInfo(id, reagentData);

            reagent.SetGridParams(Config.ReagentsParams.GridParams);

            return reagent;
        }

        public override ItemSM CreateSave(ItemInfo info)
        {
            return new ReagentItemSM(info as ReagentItemInfo);
        }

        protected override ItemInfo ReadSave(ItemData data, ItemSM save)
        {
            var reagentData = data as ReagentItemData;
            var reagentSave = save as ReagentItemSM;

            var reagent = reagentSave.GetValue(reagentData);

            reagent.SetGridParams(Config.ReagentsParams.GridParams);

            return reagent;
        }
    }
}