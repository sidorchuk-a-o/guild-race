using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Instances
{
    public class ConsumableChancesList : ListElement<ConsumableChanceData, ConsumableChanceItem>
    {
        public override void BindData(SerializedData data)
        {
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            UpdateData(data);

            base.BindData(data);
        }

        private void UpdateData(SerializedData data)
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

                var chanceDtem = new ConsumableChanceData();

                chanceDtem.SetValue("rarity", rarity);

                values.Add(chanceDtem);
            }

            data.SetValue(values);
        }
    }
}