using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class ConsumableChanceItem : ListItemElement<ConsumableChanceData>
    {
        private PropertyElement rarityField;
        private PropertyElement chanceModField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            root.ConvertToColumn();

            rarityField = root.CreateProperty();
            chanceModField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            rarityField.BindProperty("rarity", data);
            chanceModField.BindProperty("chanceMod", data);
        }
    }
}