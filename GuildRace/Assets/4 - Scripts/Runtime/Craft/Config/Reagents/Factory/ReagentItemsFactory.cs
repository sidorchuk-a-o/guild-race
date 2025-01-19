using Game.Inventory;
using System;
using VContainer;

namespace Game.Craft
{
    public class ReagentItemsFactory : ItemsFactory
    {
        private CraftConfig config;

        public override Type DataType { get; } = typeof(ReagentItemData);

        [Inject]
        public void Inject(CraftConfig config)
        {
            this.config = config;
        }

        protected override ItemInfo CreateInfo(string id, ItemData data)
        {
            var reagentData = data as ReagentItemData;
            var reagent = new ReagentItemInfo(id, reagentData);

            reagent.SetGridParams(config.ReagentsParams.GridParams);

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

            reagent.SetGridParams(config.ReagentsParams.GridParams);

            return reagent;
        }
    }
}