using System;
using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ConsumableChancesList : ListElement<ConsumableChanceData>
    {
        protected override Type ItemType { get; } = typeof(ConsumableChanceItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            UpdateData(data);
        }

        private static void UpdateData(SerializedData data)
        {
            var values = data.GetValue<List<ConsumableChanceData>>(new());
            var rarities = InventoryEditorState.Config.ItemsParams.Rarities;

            foreach (var rarityData in rarities)
            {
                var rarity = (Rarity)rarityData.Id;

                if (values.Any(x => x.Rarity == rarity))
                {
                    continue;
                }

                var chanceData = new ConsumableChanceData();

                chanceData.SetValue("rarity", rarity);

                values.Add(chanceData);
            }

            data.SetValue(values);
        }
    }
}